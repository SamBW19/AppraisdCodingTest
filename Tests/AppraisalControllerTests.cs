using Business;
using Business.Data;
using Business.Model;
using Business.Services;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class AppraisalControllerTests
    {
        [Theory]
        [InlineData(true, true, true, "Admin, SuperAdmin and OnlySuperAdminCanAddAppraisal are true")]
        [InlineData(false, true, true, "Admin is false, SuperAdmin and OnlySuperAdminCanAddAppraisal are true")]
        [InlineData(true, true, false, "Admin and SuperAdmin are true, OnlySuperAdminCanAddAppraisal is false")]
        [InlineData(true, false, false, "Admin is true, SuperAdmin and OnlySuperAdminCanAddAppraisal are false")]      
        public void successful_submission_with_different_permissions(bool isAdmin, bool isSuperAdmin, bool onlySAcanAddAppraisal, string description)
        {
            //arrange
            var newAppraisal = DummyAppraisal();

            var user = new User
            {
                IsAdmin = isAdmin,
                IsSuperAdmin = isSuperAdmin
            };

            var requestDataServiceMock = new Mock<IRequestDataService>();
            requestDataServiceMock.Setup(x => x.CurrentUser).Returns(user);

            var settinsServiceMock = new Mock<ISettingsService>();
            settinsServiceMock.Setup(x => x.Settings.OnlySuperAdminsCanAddAppraisals).Returns(onlySAcanAddAppraisal);

            var mockDatabase = new Mock<IDatabase>();
            var controller = new AppraisalController(mockDatabase.Object, requestDataServiceMock.Object, settinsServiceMock.Object);

            //act
            var result = controller.Add(newAppraisal);

            //assert
            Assert.Equal(result.IsSuccess, true);
            mockDatabase.Verify(x => x.AddNewAppraisal(It.Is<Appraisal>(y => y == newAppraisal)));
        }


        [Theory]
        [InlineData(true, false, true, "Admin and OnlySuperAdminCanAddAppraisal are true, SuperAdmin is false")]
        [InlineData(false, false, true, "Admin and SuperAdmin are false, OnlySuperAdminCanAddAppraisal is true")]
        [InlineData(false, true, false, "Admin and OnlySuperAdminCanAddAppraisal are false, SuperAdmin is true")]
        [InlineData(false, false, false, "Admin, SuperAdmin and OnlySuperAdminCanAddAppraisal are false")]
        public void unsuccessful_submission_with_different_permissions(bool isAdmin, bool isSuperAdmin, bool onlySAcanAddAppraisal, string description)
        {
            //arrange
            var newAppraisal = DummyAppraisal();

            var user = new User
            {
                IsAdmin = isAdmin,
                IsSuperAdmin = isSuperAdmin
            };

            var requestDataServiceMock = new Mock<IRequestDataService>();
            requestDataServiceMock.Setup(x => x.CurrentUser).Returns(user);

            var settinsServiceMock = new Mock<ISettingsService>();
            settinsServiceMock.Setup(x => x.Settings.OnlySuperAdminsCanAddAppraisals).Returns(onlySAcanAddAppraisal);

            var mockDatabase = new Mock<IDatabase>();
            var controller = new AppraisalController(mockDatabase.Object, requestDataServiceMock.Object, settinsServiceMock.Object);

            //act
            var result = controller.Add(newAppraisal);

            //assert
            Assert.Equal(result.IsSuccess, false);
        }


        [Theory]
        [InlineData(AdminLevel.SuperAdmin, true, "User is SuperAdmin and OnlySuperAdminCanAddAppraisal is true")]
        [InlineData(AdminLevel.Admin, false, "User is Admin and OnlySuperAdminCanAddAppraisal is false")]        
        public void alternative_successful_submission_with_different_permissions(AdminLevel adminLevel, bool onlySAcanAddAppraisal, string description)
        {
            //arrange
            var newAppraisal = DummyAppraisal();

            var user = new AlternativeUser
            {
                AdminLevel = adminLevel
            };

            var requestDataServiceMock = new Mock<IRequestDataService>();
            requestDataServiceMock.Setup(x => x.AlternativeCurrentUser).Returns(user);

            var settinsServiceMock = new Mock<ISettingsService>();
            settinsServiceMock.Setup(x => x.Settings.OnlySuperAdminsCanAddAppraisals).Returns(onlySAcanAddAppraisal);

            var mockDatabase = new Mock<IDatabase>();
            var controller = new AppraisalController(mockDatabase.Object, requestDataServiceMock.Object, settinsServiceMock.Object);

            //act
            var result = controller.AddAlternative(newAppraisal);

            //assert
            Assert.Equal(result.IsSuccess, true);
            mockDatabase.Verify(x => x.AddNewAppraisal(It.Is<Appraisal>(y => y == newAppraisal)));
        }

        [Theory]
        [InlineData(AdminLevel.Admin, true, "User is Admin and OnlySuperAdminCanAddAppraisal is true")]
        [InlineData(AdminLevel.SuperAdmin, false, "User is SuperAdmin and OnlySuperAdminCanAddAppraisal is false")]
        public void alternative_unsuccessful_submission_with_different_permissions(AdminLevel adminLevel, bool onlySAcanAddAppraisal, string description)
        {
            //arrange
            var newAppraisal = DummyAppraisal();

            var user = new AlternativeUser
            {
                AdminLevel = adminLevel
            };

            var requestDataServiceMock = new Mock<IRequestDataService>();
            requestDataServiceMock.Setup(x => x.AlternativeCurrentUser).Returns(user);

            var settinsServiceMock = new Mock<ISettingsService>();
            settinsServiceMock.Setup(x => x.Settings.OnlySuperAdminsCanAddAppraisals).Returns(onlySAcanAddAppraisal);

            var mockDatabase = new Mock<IDatabase>();
            var controller = new AppraisalController(mockDatabase.Object, requestDataServiceMock.Object, settinsServiceMock.Object);

            //act
            var result = controller.AddAlternative(newAppraisal);

            //assert
            Assert.Equal(result.IsSuccess, false);
        }



        private Appraisal DummyAppraisal()
        {
            return new Appraisal()
            {
                AppraisalID = 0,
                AppraiserID = 123,
                bAutoClosed = false,
                bDeleted = false,
                bHiddenFromAppraisee = false,
                dAutoClosed = null,
                ProcessstepID = 434,
                SignOffID = 87652,
                dDue = DateTime.UtcNow.AddDays(24),
                dCycleDate = DateTime.UtcNow.AddDays(-5),
                Title = "a third appraisal, but updated",
                UseGroupID = 4587,
                UserID = 7895
            };
        }

        private Database Database()
        {
            return new Database();
        }
    }
}
