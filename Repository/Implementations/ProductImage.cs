﻿using Microsoft.AspNetCore.Http;
using Repository.Contracts;

namespace Repository.Implementations;

public class ProductImage : IProductImage
{
    public string SaveImage(IFormFile imageFile)
    {
        string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

        imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

        var imagePath = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/", "Images/", imageName);

        var imagePathStore = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/", "Images/", imageName);

        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            imageFile.CopyTo(fileStream);
        }

        using (var fileStream = new FileStream(imagePathStore, FileMode.Create))
        {
            imageFile.CopyTo(fileStream);
        }

        return imageName;
    }

    public string UpdateImage(IFormFile imageFile, string oldImagePath)
    {
        Parallel.Invoke(
            () =>
            {
                if (File.Exists(@$"C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/Images/{oldImagePath}"))
                {
                    File.Delete(@$"C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/Images/{oldImagePath}");
                }
            },
            () =>
            {
                if (File.Exists(@$"C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/Images/{oldImagePath}"))
                {
                    File.Delete(@$"C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/Images/{oldImagePath}");
                }
            });


        string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

        imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

        var imagePath = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/", "Images/", imageName);

        var imagePathStore = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/", "Images/", imageName);


        Parallel.Invoke(
            () =>
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            },
            () =>
            {
                using (var fileStream = new FileStream(imagePathStore, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            });



        return imageName;
    }
}
