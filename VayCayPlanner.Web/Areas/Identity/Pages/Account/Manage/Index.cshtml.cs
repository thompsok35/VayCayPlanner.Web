// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Subscriber> _userManager;
        private readonly SignInManager<Subscriber> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(
            UserManager<Subscriber> userManager,
            SignInManager<Subscriber> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Enter New Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Enter New Mobile number")]
            public string Mobile_Number { get; set; }

            [Display(Name = "Default Travel Group Key")]
            public string DefaultTravelGroupKey { get; set; }

        }

        private async Task LoadAsync(Subscriber user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var existingUserName = await _userManager.GetUserNameAsync(user);
            //var role = await _userManager.GetRolesAsync(user);
            //To get the properties extended by the Subscriber data model
            var userProperties = await _userManager.GetUserAsync(User);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,                
                Mobile_Number = userProperties.Mobile_Number,
                DefaultTravelGroupKey = userProperties.DefaultTravelGroupKey
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userProperties = await _userManager.GetUserAsync(User);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            user.DefaultTravelGroupKey = Input.DefaultTravelGroupKey;
            user.Mobile_Number = Input.Mobile_Number;
            var setSubscriberResult = await _userManager.UpdateAsync(user);

            if (!setSubscriberResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update your profile.";
                
                return RedirectToPage();
            }

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
