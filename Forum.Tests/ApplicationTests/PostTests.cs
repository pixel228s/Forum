using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Application.Features.PostFeatures.Commands.CreatePost;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Posts;
using Microsoft.Extensions.Configuration;
using Moq;
using Forum.Infrastructure.Services.S3;
using Forum.Infrastructure.Services.S3.Models.Requests;
using Forum.Application.Exceptions.Models;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Forum.Application.Features.PostFeatures.Commands.ChangeState;
using Forum.Application.Exceptions;
using Forum.Domain.Models.Posts.Enums;
using Forum.Application.Features.PostFeatures.Commands.DeletePost;
using Forum.Application.Features.PostFeatures.Commands.UpdatePost;
using Forum.Domain.Entities.Posts.Enums;
using Forum.Application.Features.PostFeatures.Queries.RetrievePendingPosts;
using Forum.Domain.Parameters;
using Forum.Application.Features.PostFeatures.Queries.RetrievePost;
using Microsoft.AspNetCore.Identity;
using Forum.Domain.Models.Users;

namespace Forum.Tests.ApplicationTests
{
    public class PostTests
    {
        private readonly Mock<IPostRepository> _postRepoMock = new();
        private readonly Mock<UserManager<User>> _userManagerMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IS3Service> _s3ServiceMock = new();
        private readonly Mock<IConfiguration> _configMock = new();
        private readonly CreatePostCommandValidator _validator = new();


        private readonly CreatePostCommandHandler _handler;
        private readonly ChangeStateCommandHandler _changeStateHandler;

        public PostTests()
        {

            _configMock.Setup(c => c["AWS:Folder"]).Returns("test-folder");
            _configMock.Setup(c => c["AWS:BucketName"]).Returns("test-bucket");

            _handler = new CreatePostCommandHandler(
                _postRepoMock.Object,
                _mapperMock.Object,
                _s3ServiceMock.Object,
                _configMock.Object
            );

            _changeStateHandler = new ChangeStateCommandHandler(
                _postRepoMock.Object
            );


        }

        [Fact]
        public async Task Handle_Should_Create_Post_Without_Image()
        {
            // Arrange
            var request = new CreatePostCommand { Title = "Test", Content = "Hello", userId = 123 };
            var post = new Post { Id = 1, Title = "Test", Content = "Hello", UserId = 123 };
            var response = new PostResponse { Id = 1, Title = "Test" };

            _mapperMock.Setup(m => m.Map<Post>(request)).Returns(post);
            _mapperMock.Setup(m => m.Map<PostResponse>(post)).Returns(response);
            _postRepoMock.Setup(r => r.AddAsync(post, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            _s3ServiceMock.Verify(s => s.UploadFile(It.IsAny<FileUploadRequest>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _postRepoMock.Verify(r => r.AddAsync(post, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Empty()
        {

            var model = new CreatePostCommand { Title = "some Valid Title", Content = "", userId = 1 };
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Content)
                  .WithErrorMessage("post content can not be empty");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Too_Long()
        {
            var model = new CreatePostCommand
            {
                userId = 1,
                Title = new string('a', 301),
                Content = "Some content"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Maximum length of title exceeded");
        }

        [Fact]
        public void Should_Have_Error_When_Content_Too_Long()
        {
            var model = new CreatePostCommand
            {
                userId = 1,
                Title = "Valid Title",
                Content = new string('b', 4001)
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Content)
                  .WithErrorMessage("Maximum length of content exceeded");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            var model = new CreatePostCommand
            {
                userId = 1,
                Title = "Valid Title",
                Content = "Valid content"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task HandleShouldCreate_PostWithImage()
        {
            // Arrange
            byte[] fileBytes = new byte[10];
            var stream = new MemoryStream(fileBytes);

            IFormFile formFile = new FormFile(stream, 0, fileBytes.Length, "name", "filename.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            var request = new CreatePostCommand { Title = "vaime", Content = "Hello", userId = 123, Image = formFile };
            var post = new Post { Id = 1, Title = "vaime", Content = "Hello", UserId = 123 };
            var response = new PostResponse { Id = 1, Title = "vaime" };

            _mapperMock.Setup(m => m.Map<Post>(request)).Returns(post);
            _mapperMock.Setup(m => m.Map<PostResponse>(post)).Returns(response);
            _s3ServiceMock.Setup(s => s.UploadFile(It.IsAny<FileUploadRequest>(), "test-bucket", It.IsAny<CancellationToken>()))
                .ReturnsAsync("http://image-url");
            _postRepoMock.Setup(r => r.AddAsync(post, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal("http://image-url", post.ImageUrl);
            Assert.Equal(response, result);
            _s3ServiceMock.Verify(s => s.UploadFile(It.IsAny<FileUploadRequest>(), "test-bucket", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ChangeStateOfPost_ShouldThrowException_If_PostNotExists()
        {
            var request = new ChangeStateCommand
            {
                PostId = -1,
                IsAccepted = true
            };

            _postRepoMock
                .Setup(p => p.GetPostByIdAsync(-1, It.IsAny<CancellationToken>(), false, true))
                .ReturnsAsync((Post?)null);
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => _changeStateHandler.Handle(request, CancellationToken.None));
        }

        //[Fact]
        //public async Task DeletePostHandler_WhenPostNotExists_ShouldThow_Exception()
        //{
        //    var command = new DeletePostCommand
        //    {
        //        PostId = -1,
        //        UserId = "1",
        //    };

        //    var _handler = new DeletePostCommandHandler(_postRepoMock.Object, _userManagerMock.Object);
        //    _postRepoMock
        //        .Setup(p => p.GetPostByIdAsync(-1, It.IsAny<CancellationToken>(), false, true))
        //        .ReturnsAsync((Post?)null);
        //    await Assert.ThrowsAsync<ObjectNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        //}

        //[Fact]
        //public async Task DeletePostHandler_WhenPostExists_ButRequestedId_NotEqualsOne_InPost_ShouldThow_Exception()
        //{
        //    var command = new DeletePostCommand
        //    {
        //        PostId = 1,
        //        UserId ="1",
        //    };
        //    var post = new Post { Id = command.PostId, UserId = 2 };
        //    var _handler = new DeletePostCommandHandler(_postRepoMock.Object, _userManagerMock.Object);
        //    _postRepoMock
        //        .Setup(p => p.GetPostByIdAsync(command.PostId, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()))
        //        .ReturnsAsync(post);
        //    await Assert.ThrowsAsync<ActionForbiddenException>(() => _handler.Handle(command, CancellationToken.None));
        //}

        //[Fact]
        //public async Task DeletePostHandler_Should_RemovePost_WhenUserIsOwner()
        //{
        //    var command = new DeletePostCommand
        //    {
        //        PostId = 1,
        //        UserId = "1"
        //    };

        //    var post = new Post
        //    {
        //        Id = 1,
        //        UserId = 1
        //    };

        //    var user = new User { FirstName = "Test", LastName = "Test", Id = 1 };

        //    _postRepoMock
        //        .Setup(p => p.GetPostByIdAsync(command.PostId, It.IsAny<CancellationToken>(), false, false))
        //        .ReturnsAsync(post);

        //    _postRepoMock
        //        .Setup(p => p.RemoveAsync(post, It.IsAny<CancellationToken>()))
        //        .Returns(Task.CompletedTask);

        //    _userManagerMock.Setup(m => m.FindByIdAsync(command.UserId)).ReturnsAsync(user);

        //    var handler = new DeletePostCommandHandler(_postRepoMock.Object, _userManagerMock.Object);

        //    await handler.Handle(command, CancellationToken.None);
        //    _postRepoMock.Verify(p => p.RemoveAsync(post, It.IsAny<CancellationToken>()), Times.Once);
        //}

        [Fact]
        public async Task ChangeStateOfPost_Should_SetState_Show_When_Accepted()
        {
            var post = new Post { Id = 1, State = State.Hide };
            _postRepoMock.Setup(p => p.GetPostByIdAsync(1, It.IsAny<CancellationToken>(), false, true))
                         .ReturnsAsync(post);
            _postRepoMock.Setup(p => p.UpdateEntity(post, It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask);

            var request = new ChangeStateCommand { PostId = 1, IsAccepted = true };

            await _changeStateHandler.Handle(request, CancellationToken.None);

            Assert.Equal(State.Show, post.State);
            _postRepoMock.Verify(p => p.UpdateEntity(post, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ChangeStateOfPost_Should_SetState_Hide_When_NotAccepted()
        {
            var post = new Post { Id = 2, State = State.Show };
            _postRepoMock.Setup(p => p.GetPostByIdAsync(2, It.IsAny<CancellationToken>(), false, true))
                         .ReturnsAsync(post);
            _postRepoMock.Setup(p => p.UpdateEntity(post, It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask);

            var request = new ChangeStateCommand { PostId = 2, IsAccepted = false };

            await _changeStateHandler.Handle(request, CancellationToken.None);

            Assert.Equal(State.Hide, post.State);
            _postRepoMock.Verify(p => p.UpdateEntity(post, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePostHandler_ShouldThrowError_WhenPost_NotFount()
        {
            var commend = new UpdatePostCommand { Id = -1, UserId = 2 };
            var post = new Post { Id = commend.Id, UserId = commend.UserId, State = State.Show };
            _postRepoMock.Setup(p => p.GetPostByIdAsync(-1, It.IsAny<CancellationToken>(), false, true))
                        .ReturnsAsync((Post?)null);
            var _handler = new UpdatePostCommandHandler(_postRepoMock.Object, _mapperMock.Object);
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => _handler.Handle(commend, CancellationToken.None));
        }

        [Fact]
        public async Task UpdatePostHandler_ShouldThrowError_WhenPost_Exists_ButNotPermittedUser()
        {
            var commend = new UpdatePostCommand { Id = 11, UserId = 2 };
            var post = new Post { Id = commend.Id, UserId = 3, State = State.Show };
            _postRepoMock.Setup(p => p.GetPostByIdAsync(11, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()))
                        .ReturnsAsync(post);
            var _handler = new UpdatePostCommandHandler(_postRepoMock.Object, _mapperMock.Object);
            await Assert.ThrowsAsync<ActionForbiddenException>(() => _handler.Handle(commend, CancellationToken.None));
        }

        [Fact]
        public async Task UpdatePostHandler_ShouldThrowError_WhenPost_Exists_ButIsInactive()
        {
            var commend = new UpdatePostCommand { Id = 11, UserId = 3 };
            var post = new Post { Id = commend.Id, UserId = commend.UserId, State = State.Show, Status = Status.Inactive };
            _postRepoMock.Setup(p => p.GetPostByIdAsync(11, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()))
                        .ReturnsAsync(post);
            var _handler = new UpdatePostCommandHandler(_postRepoMock.Object, _mapperMock.Object);
            var message = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(commend, CancellationToken.None));
            Assert.Equal("Can't update an inactive post", message.Message);
        }

        [Fact]
        public async Task UpdatePostHandler_ShouldThrowValidationError_When_ContentExceeds_MaxLength()
        {
            var model = new UpdatePostCommand
            {
                Id = 11,
                UserId = 3,
                Title = "Valid Title",
                Content = new string('b', 4001)
            };
            UpdatePostCommandValidator _validator = new();
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Content)
                  .WithErrorMessage("Maximum length of content exceeded");
        }

        [Fact]
        public async Task UpdatePostHandler_ShouldThrowValidationError_When_TitleExceeds_MaxLength()
        {
            var model = new UpdatePostCommand
            {
                Id = 11,
                UserId = 3,
                Title = new string('b', 302),
                Content = "some valid content"
            };
            UpdatePostCommandValidator _validator = new();
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Maximum length of title exceeded");
        }

        [Fact]
        public async Task UpdatePost_shhouldRun_When_Everything_isOk()
        {
            var command = new UpdatePostCommand { Id = 11, UserId = 3, Title = "vaime", Content = "some Content" };
            var post = new Post { Id = command.Id, UserId = command.UserId, State = State.Show, Status = Status.Active };

            _postRepoMock
                .Setup(p => p.GetPostByIdAsync(command.Id, It.IsAny<CancellationToken>(), false, false))
                .ReturnsAsync(post);

            _mapperMock
                .Setup(m => m.Map<PostResponse>(It.IsAny<Post>()))
                .Returns((Post p) => new PostResponse { Id = p.Id, Title = p.Title, Content = p.Content });

            var handler = new UpdatePostCommandHandler(_postRepoMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            _postRepoMock.Verify(p => p.UpdateEntity(post, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal("vaime", post.Title);
            Assert.Equal("some Content", post.Content);
            Assert.Equal(post.Id, result.Id);
        }

        [Fact]
        public async Task GetAllPendingPosts_ShouldReturn_OnlyPendingPosts()
        {
            var parameters = new RequestParameters
            {
                PageNumber = 1,
                PageSize = 2
            };

            _mapperMock
                .Setup(m => m.Map<IEnumerable<PostResponse>>(It.IsAny<IEnumerable<Post>>()))
                .Returns((IEnumerable<Post> posts) => posts.Select(p => new PostResponse
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                }));

            var query = new GetPendingPostsQuery(parameters);
            var handler = new GetPendingPostsQueryHandler(_postRepoMock.Object, _mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            foreach (PostResponse postResponse in result)
            {
                Assert.Equal(State.Pending, postResponse.State);
            }
        }

        [Fact]
        public async Task GetPostById_Should_ThrowException_If_PostNotFound()
        {
            var post = new Post
            {
                Id = -1,
                Content = "some content",
                Title = "some title",
                UserId = 5
            };

            var query = new GetPostByIdQuery
            {
                PostId = post.Id,
            };

            var handler = new GetPostByIdQueryHandler(_postRepoMock.Object, _mapperMock.Object);

            var exc = await Assert.ThrowsAsync<ObjectNotFoundException>(() => handler.Handle(query, CancellationToken.None));
            Assert.Equal("Post not found!", exc.Message);
        }

        [Fact]
        public async Task GetPost_ById_When_Post_IsFound()
        {
            var post = new Post
            {
                Id = 1,
                Content = "some content",
                Title = "some title",
                UserId = 5
            };

            var query = new GetPostByIdQuery
            {
                PostId = post.Id,
            };

            _postRepoMock.Setup(p => p.GetPostByIdAsync(query.PostId, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(post);

            _mapperMock
                       .Setup(m => m.Map<PostResponse>(It.IsAny<Post>()))
                       .Returns((Post p) => new PostResponse { Id = p.Id, Title = p.Title, Content = p.Content, AuthorId = p.UserId });

            var handler = new GetPostByIdQueryHandler(_postRepoMock.Object, _mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);
            Assert.Equal(post.Id, result.Id);
            Assert.Equal(post.Content, result.Content); 
            Assert.Equal(post.Title, result.Title);
            Assert.Equal(post.UserId, result.AuthorId);
        }

    }
}