using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Models
{
    public class MainViewModel
    {
        public SaveModel SaveModel { get; set; } = new SaveModel();
        public SaveModel StorageModel { get; set; } = new SaveModel() { IsStorage = true };
        public MainViewModel()
        {
            SaveModel.Init(this);
            StorageModel.Init(this);
        }
    }
}
