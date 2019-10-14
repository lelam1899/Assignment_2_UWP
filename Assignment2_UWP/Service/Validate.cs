using Assignment2_UWP.Entity;
using Microsoft.Toolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assignment2_UWP.Service
{
    class Validate
    {
        public static Dictionary<string, string> errors = new Dictionary<string, string>();

        public static Dictionary<string, string> ValidateMember(MemberRegister memberRegister)
        {
            errors.Clear();
            if (string.IsNullOrEmpty(memberRegister.firstName))
            {
                errors.Add("FirstName", "FirstName is required!");
            }
            if (string.IsNullOrEmpty(memberRegister.lastName))
            {
                errors.Add("LastName", "LastName is required!");
            }
            if (string.IsNullOrEmpty(memberRegister.avatar))
            {
                errors.Add("Avatar", "Avatar is required!");
            }
            if (!memberRegister.phone.IsPhoneNumber())
            {
                errors.Add("Phone", "Please enter the number!");
            }
            else if (string.IsNullOrEmpty(memberRegister.phone))
            {
                errors.Add("Phone", "Phone is required!");
            }
            if (string.IsNullOrEmpty(memberRegister.address))
            {
                errors.Add("Address", "Address is required!");
            }
            if (string.IsNullOrEmpty(memberRegister.introduction))
            {
                errors.Add("Introduction", "Introduction is required!");
            }

            if (memberRegister.birthday == "01-01-01")
            {
                errors.Add("Birthday", "Birthday is required!");
            }
            if (memberRegister.gender != 0 && memberRegister.gender != 1)
            {
                errors.Add("Gender", "Gender is required!");
            }
            if (!memberRegister.email.IsEmail())
            {
                errors.Add("Email", "Please enter a vaild email address!");
            }
            if (string.IsNullOrEmpty(memberRegister.password))
            {
                errors.Add("Password", "Password is required!");
            }
            else if (memberRegister.password.Length < 8)
            {
                errors.Add("Password", "Password must be more than 8 characters!");
            }


            return errors;
        }




        public static Dictionary<string, string> ValidateLogin(MemberLogin memberLogin)
        {
            errors.Clear();
            if (string.IsNullOrEmpty(memberLogin.email) || !Regex.IsMatch(memberLogin.email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                errors.Add("Email", "Email is required!");
            }

            if (string.IsNullOrEmpty(memberLogin.password))
            {
                errors.Add("Password", "Password is required!");
            }

            return errors;
        }
        public static Dictionary<string, string> ValidateSong(Song song)
        {


            errors.Clear();
            if (string.IsNullOrEmpty(song.name))
            {
                errors.Add("Name", "Name is required!");
            }
            else if (song.name.Length > 50)
            {
                errors.Add("Name", "Max 50 characters!");
            }

            if (string.IsNullOrEmpty(song.thumbnail))
            {
                errors.Add("Thumbnail", "Image is required!");
            }
            if (string.IsNullOrEmpty(song.author))
            {
                errors.Add("Author", "Author is required!");
            }
            if (string.IsNullOrEmpty(song.description))
            {
                errors.Add("Description", "Description is required!");
            }

            if (string.IsNullOrEmpty(song.singer))
            {
                errors.Add("Singer", "Singer is required!");
            }
            if (string.IsNullOrEmpty(song.link))
            {
                errors.Add("Link", "Link is required!");
            }

            return errors;
        }


    }
}
