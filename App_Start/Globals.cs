using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assigner.App_Start
{
    public enum Roles
    {
        Student, Teacher
    }

    public class Globals
    {
        public static Roles Role { get; set; }
    }
}