using ContactManagerApi.Models;
using ContactManger.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;

namespace ContactManger.Tests
{
    [TestClass]
    public class ContactTest
    {

        private ContactContext _contactsContext;
        private IContactService constactService;


        public ContactTest()
        {
            InitContext();
        }

        public void InitContext()
        {

            IList<Contacts> contacts = GenerateNotLockedUsers();
            var contactContext = new Mock<ContactContext>();

            contactContext.Setup(x => x.Contacts).ReturnsDbSet(contacts);
            
        }

        [TestMethod]
        public async Task AddContact_Should_Return_Error_When_FirstNameIsEmpty()
        {
            //Arrange
                  ContactRequest request = new ContactRequest();
            
            try
            {
                //Act
                var result = await constactService.AddContact(request);
            }
            catch (Exception ex)
            {
                //Test
                Assert.IsNotNull(ex.Message);
            }
        }

        [TestMethod]
        public async Task AddContact_Should_AddandReturn_Valid_ContactId()
        {

              //Arrange
            ContactRequest request = new ContactRequest();
            request.FirstName = "testF1";
            request.LastName = "testLName";
            request.Email = "testEmail@test.com";
            request.PhoneNumber = "1234567890";

            
            int result = 0;
            try
            {
                result = await constactService.AddContact(request);
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }

            Assert.IsTrue(result > 0);
        }

        private static IList<Contacts> GenerateNotLockedUsers()

        {

            IList<Contacts> contacts = new List<Contacts>

            {

               
            };



            return contacts;

        }


    }


}
