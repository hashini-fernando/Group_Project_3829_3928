using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;


namespace Final_Group_Project.Models
{
    public class Module
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public int Credit { get; set; }

        public ObservableCollection<StudentModule> StudentModules { get; set; }

        public Module() { }
    }
}
