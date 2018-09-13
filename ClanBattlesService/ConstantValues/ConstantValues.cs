using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanBattlesService.ConstantValues
{
   
        public class EntityStatus
        {
            public static string ACTIVE = "ACT";
            public static string INACTIVE = "INA";
        }

        public class ResponseStatus
        {
            public static string OK = "ok";
            public static string ERROR = "error";
        }

        public class ErrorMessage
        {
            public static string INTERNAL_SERVER_ERROR = "Something went wrong";
            public static string BAD_REQUEST = "Body Parameters are not valid";
            public static string NOT_FOUND = "Entity {0} with ID {1} not found";
        }
    
}