using AP204_Pronia.Models;
using AP204_Pronia.Services;
using System.Collections.Generic;

namespace AP204_Pronia.ViewModels
{
    public class HomeVB
    {
        public List<Slider> Sliders { get; set; }

        public List<Plant> Plants { get; set; }


        public List<Setting> settings { get; set; }

        public List <AppUser>  appUsers{ get; set; }

        public List <Catagory> catagories{ get; set; }

        public List <Color> colors{ get; set; }

        public List <PlantCatagory> plantCatagories{ get; set; }

        public List <PlantImage> plantImages{ get; set; }
    }
}
