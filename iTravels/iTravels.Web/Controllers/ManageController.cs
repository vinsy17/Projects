using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
using iTravels.Web.ViewModels;
using Sample.Core.Services;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Security;
using iTravels.Web.Utilities;
using Sample.Core.Models;

namespace iTravels.Web.Controllers
{
    [iTAuthorize]
    public class ManageController : Controller
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;
        private readonly IUserService _userService;

        private bool IsIdentityEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IsIdentityEnabled"].ToString());
        private Guid UserId = Utilities.Utility.GetUserIdFromSession();// System.Web.HttpContext.Current.Session["UserId"] != null ? new Guid(System.Web.HttpContext.Current.Session["UserId"].ToString()) : ;
        public ManageController()
        {
        }
        public ManageController(IUserService userService)
        {
            this._userService = userService;
        }

        //public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            if (IsIdentityEnabled)
            {
                //var userId = User.Identity.GetUserId();
                //var model = new IndexViewModel
                //{
                //    HasPassword = HasPassword(),
                //    PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                //    TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                //    Logins = await UserManager.GetLoginsAsync(userId),
                //    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                //};
                //return View(model);
            }
            else
            {
                User user = UserId != Guid.Empty ? _userService.GetUser(UserId) : null;
                if (user != null)
                {
                    var model = new IndexViewModel
                    {
                        HasPassword = true,//HasPassword(),
                        PhoneNumber = user.MobileNumber,// await UserManager.GetPhoneNumberAsync(userId),
                        TwoFactor = false,//await UserManager.GetTwoFactorEnabledAsync(userId),
                        //Logins = new List<UserLoginInfo>(),// await UserManager.GetLoginsAsync(userId),
                        BrowserRemembered = false// await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                    };
                    return View(model);
                }
                else {
                    return View(new IndexViewModel());
                }
            }

            return View(new IndexViewModel());
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message=null;
            if (IsIdentityEnabled)
            {
                //var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
                //if (result.Succeeded)
                //{
                //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //    if (user != null)
                //    {
                //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //    }
                //    message = ManageMessageId.RemoveLoginSuccess;
                //}
                //else
                //{
                //    message = ManageMessageId.Error;
                //}
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (IsIdentityEnabled)
            {
                //// Generate the token and send it
                //var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
                //if (UserManager.SmsService != null)
                //{
                //    var message = new IdentityMessage
                //    {
                //        Destination = model.Number,
                //        Body = "Your security code is: " + code
                //    };
                //    await UserManager.SmsService.SendAsync(message);
                //}
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            if (IsIdentityEnabled)
            {
                //await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
                //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //if (user != null)
                //{
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //}
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            if (IsIdentityEnabled)
            {
                //await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
                //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //if (user != null)
                //{
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //}
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            if (IsIdentityEnabled)
            {
                //var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            }
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (IsIdentityEnabled)
            {
                //var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
                //if (result.Succeeded)
                //{
                //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //    if (user != null)
                //    {
                //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //    }
                //    return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
                //}
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            if (IsIdentityEnabled)
            {
                //var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
                //if (!result.Succeeded)
                //{
                //    return RedirectToAction("Index", new { Message = ManageMessageId.Error });
                //}
                //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //if (user != null)
                //{
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //}
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (IsIdentityEnabled)
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(model);
                //}
                //var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                //if (result.Succeeded)
                //{
                //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //    if (user != null)
                //    {
                //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //    }
                //    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                //}
                //AddErrors(result);
            }
            else
            {
                if (ModelState.IsValid && UserId != new Guid())
                {
                    bool isSuccess = _userService.ChangePassword(UserId, model.OldPassword, model.NewPassword);
                    if (isSuccess)
                    {
                        string emailAddress = Utilities.Utility.GetEmailAddressFromSession();
                        var user = _userService.AuthenticateUser(emailAddress, model.NewPassword);
                        if (user != null && user.IsAuthenticateUser)
                        {
                            FormsAuthentication.SignOut();
                            bool isPersistent = false;
                            int timeout = 30; // Timeout in minutes, 525600 = 365 days.
                            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, user.EmailAddress, DateTime.Now, DateTime.Now.AddMinutes(timeout), isPersistent, "your custom data");
                            string cookiestr = FormsAuthentication.Encrypt(tkt);
                            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                            if (isPersistent)
                                ck.Expires = tkt.Expiration;
                            ck.Path = FormsAuthentication.FormsCookiePath;
                            //    ck.HttpOnly = true; // cookie not available in javascript.
                            Response.Cookies.Add(ck);
                            //session values
                            HttpContext.Session.Add("UserId", user.UserId);
                            HttpContext.Session.Add("EmailAddress", user.UserId);
                            return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                        }
                    }
                    ModelState.AddModelError("", "Error in Changing Password");
                }

            }
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (IsIdentityEnabled)
                {
                    //var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    //if (result.Succeeded)
                    //{
                    //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    //    if (user != null)
                    //    {
                    //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //    }
                    //    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                    //}
                    //AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            if (IsIdentityEnabled)
            {
                //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //if (user == null)
                //{
                //    return View("Error");
                //}
                //var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
                //var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
                //ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
                //return View(new ManageLoginsViewModel
                //{
                //    CurrentLogins = userLogins,
                //    OtherLogins = otherLogins
                //});
            }
            return View();
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            if (IsIdentityEnabled)
            {
                //// Request a redirect to the external login provider to link a login for the current user
                //return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
            }
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), Utility.GetUserIdFromSession().ToString());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            if (IsIdentityEnabled)
            {
                //var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
                //if (loginInfo == null)
                //{
                //    return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
                //}
                //var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);

                //return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            return RedirectToAction("ManageLogins");
        }

        protected override void Dispose(bool disposing)
        {
            if (IsIdentityEnabled)
            {
                //if (disposing && _userManager != null)
                //{
                //    _userManager.Dispose();
                //    _userManager = null;
                //}
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        private bool HasPassword()
        {
            if (IsIdentityEnabled)
            {
                //var user = UserManager.FindById(User.Identity.GetUserId());
                //if (user != null)
                //{
                //    return user.PasswordHash != null;
                //}
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            if (IsIdentityEnabled)
            {
                //var user = UserManager.FindById(User.Identity.GetUserId());
                //if (user != null)
                //{
                //    return user.PhoneNumber != null;
                //}
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}