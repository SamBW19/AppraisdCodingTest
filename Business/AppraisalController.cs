using Business.Data;
using Business.Model;
using Business.Services;
using System;

namespace Business
{
    /// <summary>
    /// Squint your eyes and you could imagine this was a real MVC controller
    /// </summary>
    public class AppraisalController
    {
        public AppraisalController(IDatabase database, IRequestDataService requestDataService, ISettingsService settingsService)
        {
            this._requestDataService = requestDataService;
            this._settingsService = settingsService;
            this._database = database;
        }

        private IRequestDataService _requestDataService;
        private readonly ISettingsService _settingsService;
        private IDatabase _database;

        public JsonResultDummy Add(Appraisal newApparisal)
        {
            try
            {
                if(newApparisal == null)
                {
                    return new JsonResultDummy(false, "Appraisal object is null");
                }

                var onlySuperAdminsCanAddAppraisals = _settingsService.Settings.OnlySuperAdminsCanAddAppraisals;
                var currentUser = _requestDataService.CurrentUser;

                var userHasAdminAccess = CheckAdminAccess(currentUser);

                if (!userHasAdminAccess)
                {
                    return new JsonResultDummy(false, GenerateUnsuccessfulMessage(currentUser.UserID));
                }

                if (onlySuperAdminsCanAddAppraisals && !currentUser.IsSuperAdmin)
                {
                    return new JsonResultDummy(false, GenerateUnsuccessfulMessage(currentUser.UserID));
                }

                if (!onlySuperAdminsCanAddAppraisals && !currentUser.IsAdmin && currentUser.IsSuperAdmin)
                {
                    return new JsonResultDummy(false, "Permission denied. Only admins can add appraisals when OnlySuperAdminsCanAddAppraisals is false.");
                }

                _database.AddNewAppraisal(newApparisal);
                return new JsonResultDummy(true, GenerateSuccessMessage(currentUser.UserID));
            }
            catch (Exception ex)
            {
                return new JsonResultDummy(false, $"Error: {ex.Message}");
            }
          
        }


        public JsonResultDummy AddAlternative(Appraisal newApparisal)
        {
            try
            {
                if (newApparisal == null)
                {
                    return new JsonResultDummy(false, "Appraisal object is null");
                }

                var onlySuperAdminsCanAddAppraisals = _settingsService.Settings.OnlySuperAdminsCanAddAppraisals;
                var currentUser = _requestDataService.AlternativeCurrentUser;

                var userHasAdminAccess = CheckAdminAccessAlt(currentUser);

                if (!userHasAdminAccess)
                {
                    return new JsonResultDummy(false, GenerateUnsuccessfulMessage(currentUser.UserID));
                }

                if (onlySuperAdminsCanAddAppraisals && currentUser.AdminLevel != AdminLevel.SuperAdmin)
                {
                    return new JsonResultDummy(false, GenerateUnsuccessfulMessage(currentUser.UserID));
                }

                if (!onlySuperAdminsCanAddAppraisals && currentUser.AdminLevel == AdminLevel.SuperAdmin)
                {
                    return new JsonResultDummy(false, "Permission denied. Only admins can add appraisals when OnlySuperAdminsCanAddAppraisals is false.");
                }

                _database.AddNewAppraisal(newApparisal);
                return new JsonResultDummy(true, GenerateSuccessMessage(currentUser.UserID));
            }
            catch (Exception ex)
            {
                return new JsonResultDummy(false, $"Error: {ex.Message}");
            }
        }

        private bool CheckAdminAccess(User currentUser)
        {
            if(currentUser.IsAdmin || currentUser.IsSuperAdmin)
            {
                return true;
            }
            return false;
        }

        private bool CheckAdminAccessAlt(AlternativeUser currentUser)
        {
            if (currentUser.AdminLevel == AdminLevel.Admin || currentUser.AdminLevel == AdminLevel.SuperAdmin)
            {
                return true;
            }
            return false;
        }

        private string GenerateSuccessMessage(int userID)
        {
            return $"Appraisal has been added by user {userID}.";
        }

        private string GenerateUnsuccessfulMessage(int userID)
        {
            return $"Permission denied, user: {userID} does not have the admin permissions to add an appraisal:";
        }
    }
}
