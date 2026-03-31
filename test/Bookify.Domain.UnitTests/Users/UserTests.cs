using Bookify.Domain.Users;
using Bookify.Domain.Users.Events;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.UnitTests.Users
{
   public class UserTests  
    {
        [Fact]
        public void Create_Should_SetPropertyValue()
        {
            // Act
            var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            // Assert
            user.FirstName.Should().Be(UserData.FirstName);
            user.LastName.Should().Be(UserData.LastName);
            user.Email.Should().Be(UserData.Email);
        }

        [Fact]
        public void Create_Should_RaiseUserCreatedDomainEvent()
        {
            // Act
            var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            // Assert

          var domainEvent =  user.GetDomainEvents().OfType<UserCreatedDomainEvents>().SingleOrDefault();

            domainEvent.UserId.Should().Be(user.Id);    
          
        }
    }
}
