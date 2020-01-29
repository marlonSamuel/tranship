using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransShip.Models
{
    public class Repository
    {
        private static Dictionary<string, Usuario> responses;

        static Repository()
        {
            responses = new Dictionary<string, Usuario>
            {
                {
                    "mauro",
                    new Usuario
                    {
                        Password = "123",
                        Email = "mortega@transship.com"
                    }
                },
                {
                    "tomas",
                    new Usuario
                    {
                        Password = "123",
                        Email = "bill@transship.com"
                    }
                }
            };
        }

        public static void Add(Usuario newResponse)
        {
            string key = newResponse.Email.ToLowerInvariant();
            if (responses.ContainsKey(key))
            {
                responses[key] = newResponse;
            }
            else
            {
                responses.Add(key, newResponse);
            }
        }

        public static IEnumerable<Usuario> Responses
        {
            get { return responses.Values; }
        }
    }



    public class Usuario
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }        
    }
}