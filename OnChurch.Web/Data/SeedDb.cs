using OnChurch.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCampusesAsync();
        }


        private async Task CheckCampusesAsync()
        {
            if(!_context.Campuses.Any())
            {
                _context.Campuses.Add(new Campus { 
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
