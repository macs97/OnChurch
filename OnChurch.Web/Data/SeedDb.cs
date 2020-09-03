using OnChurch.Common.Entities;
using OnChurch.Common.Enum;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCampusesAsync();
            await CheckProfessionsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Marcela", "Cardona", "marcela.alexandra97@gmail.com", "3507794247", "Calle casa", UserType.Admin);

        }


        private async Task CheckProfessionsAsync() 
        {
            _context.Professions.Add(new Profession
            {
                Name = "Sacerdote"
            });

            _context.Professions.Add(new Profession
            {
                Name = "Obispo"
            });
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<Member> CheckUserAsync(
           string document,
           string firstName,
           string lastName,
           string email,
           string phone,
           string address,
           UserType userType)
        {
            Member member = await _userHelper.GetMemberAsync(email);
            if (member == null)
            {
                member = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    Church = _context.Churches.FirstOrDefault(),
                    Profession = _context.Professions.FirstOrDefault(),                    
                    UserType = userType
                };

                await _userHelper.AddMemberAsync(member, "123456");
                await _userHelper.AddMemberToRoleAsync(member, userType.ToString());
            }

            return member;
        }


        private async Task CheckCampusesAsync()
        {
            if (!_context.Campuses.Any())
            {
                _context.Campuses.Add(new Campus
                {
                    Name = "Antioquia",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Medellin",
                            Churches = new List<Church>
                            {
                                new Church { Name = "Parroquia Nuestra Señora del Sufragio"},
                                new Church { Name = "Iglesia de la Veracruz"},
                                new Church { Name = "El Sagrado corazón de Jesús"},
                                new Church { Name = "Iglesia San Agustín"}
                            }
                        },
                        new Section
                        {
                            Name = "Sabaneta",
                            Churches = new List<Church>
                            {
                                new Church { Name = "Nuestra Señora de los Dolores"},
                                new Church { Name = "Santa Ana -  Santuario de María Auxiliadora"},
                                new Church { Name = "Nuestra Señora del Carmen"},
                                new Church { Name = "San Felipe Apostol"}
                            }
                        },
                        new Section
                        {
                            Name = "Envigado",
                            Churches = new List<Church>
                            {
                                new Church { Name = "Santa Gertrudis"},
                                new Church { Name = "La Niña María"},
                                new Church { Name = "Santa Bárbara de la Ayurá"}
                            }
                        }
                        ,
                        new Section
                        {
                            Name = "Copacabana",
                            Churches = new List<Church>
                            {
                                new Church { Name = "Iglesia Católica La Asunción"},
                                new Church { Name = "Iglesia Católica Nuestra Señora de la Ternura"}
                            }
                        }
                    }
                });

                _context.Campuses.Add(new Campus
                {
                    Name = "Cundinamarca",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Bogotá",
                            Churches = new List<Church>
                            {
                                new Church { Name = "Basílica Menor Nuestra Señora de Lourdes" },
                                new Church { Name = "Iglesia de Nuestra Señora de la Candelaria" },
                                new Church { Name = "Iglesia Santa Barbara Centro" }
                            }
                        },
                        new Section
                        {
                            Name = "Suba",
                            Churches = new List<Church>
                            {
                                new Church { Name = "Iglesia de la Inmaculada Concepción de Suba" },
                                new Church { Name = "San Calixto" }
                            }
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
