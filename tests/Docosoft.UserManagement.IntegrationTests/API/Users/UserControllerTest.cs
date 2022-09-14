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
            var createCommand = new CreateUserCommand("Test Name", "Test Last Name", "Female", "Email", ApiUsersRoleId);
            var client = _factory.CreateClient();

            var postResponse = await client.PostAsJsonAsync<CreateUserCommand>("api/Users", createCommand);
            var userDto = await postResponse.Content.ReadFromJsonAsync<UserDto>();

            // Act
            var deleteResponse = await client.DeleteAsync($"api/Users/{userDto.Id}");
            var deleteUserDto = deleteResponse.Content.ReadFromJsonAsync<UserDto>();
            // var getResponse = await client.GetAsync("api/Users");
            // var allUsersResponse = await getResponse.Content.ReadFromJsonAsync<IList<UserDto>>();
            
            // Assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.NotNull(userDto);
            Assert.NotNull(userDto.Id);
            Assert.Equal(userDto.FirstName, createCommand.FirstName);
            Assert.Equal(userDto.LastName, createCommand.LastName);
            Assert.Equal(userDto.Email, createCommand.Email);
            Assert.Equal(userDto.UserRoleId, createCommand.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_PUT_ShouldCreateNewUser()
        {
            // Arrange
            var userId = new Guid("a80367f2-80cd-4e1c-8532-08770c73f3c8");
            var updateCommand = new UpdateUserCommand(userId, "Test Name", "Test Last Name", "Female", "Email", ApiUsersRoleId);
            var client = _factory.CreateClient();
            
            // Act
            var putResponse = await client.PutAsJsonAsync<UpdateUserCommand>("api/Users", updateCommand);
            var userDto = await putResponse.Content.ReadFromJsonAsync<UserDto>();

            var deleteResponse = await client.DeleteAsync($"api/Users/{userDto.Id}");
            var deleteUserDto = await deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode); // should be created because used didn't exist in the system
            Assert.NotNull(userDto);
            Assert.Equal(userDto.Id, userId);
            Assert.Equal(userDto.FirstName, updateCommand.FirstName);
            Assert.Equal(userDto.LastName, updateCommand.LastName);
            Assert.Equal(userDto.Email, updateCommand.Email);
            Assert.Equal(userDto.UserRoleId, updateCommand.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_PUT_ShouldUpdateUser()
        {
            // Arrange
            var createCommand = new CreateUserCommand("Test Name", "Test Last Name", "Female", "Email", ApiUsersRoleId);
            
            var client = _factory.CreateClient();

            // Act
            var createResponse = await client.PostAsJsonAsync<CreateUserCommand>("api/Users", createCommand);
            var createUserDto = await createResponse.Content.ReadFromJsonAsync<UserDto>();

            var updateCommand = new UpdateUserCommand(createUserDto.Id, "Test Name 1", "Test Last Name 1", "Male", "Email", ApiUsersRoleId);

            var putResponse = await client.PutAsJsonAsync<UpdateUserCommand>("api/Users", updateCommand);
            var putUserDto = await putResponse.Content.ReadFromJsonAsync<UserDto>();

            var deleteResponse = await client.DeleteAsync($"api/Users/{putUserDto.Id}");
            var deleteUserDto = await deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode); // should be OK because user existed before PUT
            Assert.NotNull(putUserDto);
            Assert.Equal(putUserDto.Id, createUserDto.Id);
            Assert.Equal(putUserDto.FirstName, updateCommand.FirstName);
            Assert.Equal(putUserDto.LastName, updateCommand.LastName);
            Assert.Equal(putUserDto.Email, updateCommand.Email);
            Assert.Equal(putUserDto.UserRoleId, updateCommand.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_GET_ShouldReturnCreatedUser()
        {
            // Arrange
            var createCommand = new CreateUserCommand("Test Name", "Test Last Name", "Female", "Email", ApiUsersRoleId);
            
            var client = _factory.CreateClient();

            // Act
            var createResponse = await client.PostAsJsonAsync<CreateUserCommand>("api/Users", createCommand);
            var createUserDto = await createResponse.Content.ReadFromJsonAsync<UserDto>();
            
            var getQuery = new GetUserQuery(createUserDto.Id);
            var getResponse = await client.GetAsync($"api/Users/{createUserDto.Id}" );
            var getUserDto = await getResponse.Content.ReadFromJsonAsync<UserDto>();

            var deleteResponse = await client.DeleteAsync($"api/Users/{getUserDto.Id}");
            var deleteUserDto = await deleteResponse.Content.ReadFromJsonAsync<UserDto>();

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode); // should be OK because user existed before PUT
            Assert.NotNull(getUserDto);
            Assert.Equal(getUserDto.Id, createUserDto.Id);
            Assert.Equal(getUserDto.FirstName, createCommand.FirstName);
            Assert.Equal(getUserDto.LastName, createCommand.LastName);
            Assert.Equal(getUserDto.Email, createCommand.Email);
            Assert.Equal(getUserDto.UserRoleId, createCommand.UserRoleId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteUserDto);
            Assert.NotNull(deleteUserDto.Id);
        }

        [Fact]
        public async void UserControllerTest_GET_ShouldReturn2Users()
        {
            // Arrange
            var createCommand = new CreateUserCommand("Test Name", "Test Last Name", "Female", "Email", ApiUsersRoleId);
            
            var client = _factory.CreateClient();

            // Act
            var createResponse = await client.PostAsJsonAsync<CreateUserCommand>("api/Users", createCommand);
            var createUserDto = await createResponse.Content.ReadFromJsonAsync<UserDto>();
            
            var getAllUsersQuery = new GetAllUsersQuery(0,10);
            var getResponse = await client.GetAsync($"api/Users" );
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