﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        public static IEnumerable<SelectListItem> AvailableManufacturers
        {
            get
            {
                var manufacturerRepo = new ManufacturerRepository();
                var manufacturers = manufacturerRepo.GetAll().Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }).ToList();
                manufacturers.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
                return manufacturers;
            }
        }

        public AccountController()
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (IsModelStateValidValid())
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe, Response);
                    if (!String.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "ProductCategory");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        protected virtual bool IsModelStateValidValid()
        {
            return ModelState.IsValid;
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "ProductCategory");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index", "ProductCategory");
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index", "ProductCategory");

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email, model.Role, model.AssociatedManufacturerId);

                if (createStatus == MembershipCreateStatus.Success)
                    return RedirectToAction("Index", "ProductCategory");
                ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                    return RedirectToAction("ChangePasswordSuccess");
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index", "ProductCategory");

            var userRepo = new UserRepository();
            var user = userRepo.GetById(id);
            var success = user != null && MembershipService.DeleteUser(user.UserName);
            return View(success);
        }

        public ActionResult Manage()
        {
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index", "ProductCategory");
            
            var allUser = new UserRepository().GetAll();
            return View(allUser);
        }

    }
}
