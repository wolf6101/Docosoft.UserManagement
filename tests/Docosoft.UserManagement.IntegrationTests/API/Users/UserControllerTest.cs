using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Collections.Generic;
using Docosoft.UserManagement.Application.Users;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Docosoft.UserManagement.IntegrationTests.API.Users
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private Guid ApiUsersRoleId = new Guid("0813e00f-d49b-4675-b74a-0ab63bcf7404"); //seeded in db context

        public UserControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                });
        }

        [Fact]
        public async void UserControllerTest_POST_ShouldCreateNewUser()
        {
            // Arrange
            var createRequest = new CreateUserRequestDto()
            {
                FirstName = "Test Name",
                LastName = "Test Last Name",
                Gender = "Female",
                Email = "Email",
                UserRoleId = ApiUsersRoleId
            };
            var client = _factory.CreateClient();

            var postResponse = await client.PostAsJsonAsync<CreateUserRequestDto>("api/Users", createRequest);
            var userDto = await postResponse.Content.ReadFromJsonAsync<UserDto>();

            // Act
            var deleteResponse = await client.DeleteAsync($"api/Users/{userDto.Id}");
            var deleteUserDto = deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.NotNull(userDto);
            Assert.NotNull(userDto.Id);
            Assert.Equal(userDto.FirstName, createRequest.FirstName);
            Assert.Equal(userDto.LastName, createRequest.LastName);
            Assert.Equal(userDto.Email, createRequest.Email);
            Assert.Equal(userDto.UserRoleId, createRequest.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_PUT_ShouldCreateNewUser()
        {
            // Arrange
            var userId = new Guid("a80367f2-80cd-4e1c-8532-08770c73f3c8");
            var updateRequest = new UpdateUserRequestDto()
            {
                Id = userId,
                FirstName = "Test Name",
                LastName = "Test Last Name",
                Gender = "Female",
                Email = "Email",
                UserRoleId = ApiUsersRoleId
            };
            var client = _factory.CreateClient();

            // Act
            var putResponse = await client.PutAsJsonAsync<UpdateUserRequestDto>("api/Users", updateRequest);
            var userDto = await putResponse.Content.ReadFromJsonAsync<UserDto>();

            var deleteResponse = await client.DeleteAsync($"api/Users/{userDto.Id}");
            var deleteUserDto = await deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode); // should be created because used didn't exist in the system
            Assert.NotNull(userDto);
            Assert.Equal(userDto.Id, userId);
            Assert.Equal(userDto.FirstName, updateRequest.FirstName);
            Assert.Equal(userDto.LastName, updateRequest.LastName);
            Assert.Equal(userDto.Email, updateRequest.Email);
            Assert.Equal(userDto.UserRoleId, updateRequest.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_PUT_ShouldUpdateUser()
        {
            // Arrange
            var createRequest = new CreateUserRequestDto()
            {
                FirstName = "Test Name",
                LastName = "Test Last Name",
                Gender = "Female",
                Email = "Email",
                UserRoleId = ApiUsersRoleId
            };
            var client = _factory.CreateClient();

            // Act
            var createResponse = await client.PostAsJsonAsync<CreateUserRequestDto>("api/Users", createRequest);
            var createUserDto = await createResponse.Content.ReadFromJsonAsync<UserDto>();

            var updateRequest = new UpdateUserRequestDto()
            {
                Id = createUserDto.Id,
                FirstName = "Test Name 1",
                LastName = "Test Last Name 1",
                Gender = "Male",
                Email = "Email 1",
                UserRoleId = ApiUsersRoleId
            };
            var putResponse = await client.PutAsJsonAsync<UpdateUserRequestDto>("api/Users", updateRequest);
            var putUserDto = await putResponse.Content.ReadFromJsonAsync<UserDto>();

            var deleteResponse = await client.DeleteAsync($"api/Users/{putUserDto.Id}");
            var deleteUserDto = await deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode); // should be OK because user existed before PUT
            Assert.NotNull(putUserDto);
            Assert.Equal(putUserDto.Id, createUserDto.Id);
            Assert.Equal(putUserDto.FirstName, updateRequest.FirstName);
            Assert.Equal(putUserDto.LastName, updateRequest.LastName);
            Assert.Equal(putUserDto.Email, updateRequest.Email);
            Assert.Equal(putUserDto.UserRoleId, updateRequest.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_GET_ShouldReturnCreatedUser()
        {
            // Arrange
            var createRequest = new CreateUserRequestDto()
            {
                FirstName = "Test Name",
                LastName = "Test Last Name",
                Gender = "Female",
                Email = "Email",
                UserRoleId = ApiUsersRoleId
            };
            var client = _factory.CreateClient();

            // Act
            var createResponse = await client.PostAsJsonAsync<CreateUserRequestDto>("api/Users", createRequest);
            var createUserDto = await createResponse.Content.ReadFromJsonAsync<UserDto>();

            var getResponse = await client.GetAsync($"api/Users/{createUserDto.Id}");
            var getUserDto = await getResponse.Content.ReadFromJsonAsync<UserDto>();

            var deleteResponse = await client.DeleteAsync($"api/Users/{getUserDto.Id}");
            var deleteUserDto = await deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode); // should be OK because user existed before PUT
            Assert.NotNull(getUserDto);
            Assert.Equal(getUserDto.Id, createUserDto.Id);
            Assert.Equal(getUserDto.FirstName, createRequest.FirstName);
            Assert.Equal(getUserDto.LastName, createRequest.LastName);
            Assert.Equal(getUserDto.Email, createRequest.Email);
            Assert.Equal(getUserDto.UserRoleId, createRequest.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_GET_ShouldReturn2Users()
        {
            // Arrange
            var createRequest = new CreateUserRequestDto()
            {
                FirstName = "Test Name",
                LastName = "Test Last Name",
                Gender = "Female",
                Email = "Email",
                UserRoleId = ApiUsersRoleId
            };
            var client = _factory.CreateClient();

            // Act
            var createResponse = await client.PostAsJsonAsync<CreateUserRequestDto>("api/Users", createRequest);
            var createUserDto = await createResponse.Content.ReadFromJsonAsync<UserDto>();

            var getResponse = await client.GetAsync($"api/Users");
            var getUsersDtos = await getResponse.Content.ReadFromJsonAsync<IList<UserDto>>();

            var deleteResponse = await client.DeleteAsync($"api/Users/{createUserDto.Id}");
            var deleteUserDto = await deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode); // should be OK because user existed before PUT
            Assert.NotNull(getUsersDtos);
            Assert.Equal(getUsersDtos.Count, 2); // added in the test + SuperUser from dbcontext seed

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }
    }
}