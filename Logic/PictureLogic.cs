﻿using Logic.Helper;
using Logic.Interfaces;
using Models;
using Models.Dtos.Picture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repo;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
namespace Logic
{
    public class PictureLogic
    {
        IRepository<Picture> pictureRepo;
        DtoProvider dtoProvider;

        public PictureLogic(IRepository<Picture> pictureRepo, DtoProvider dtoProvider)
        {
            this.pictureRepo = pictureRepo;
            this.dtoProvider = dtoProvider;

        }

        public Picture GetPictureEntity(string id)
        {
            return pictureRepo.Read(id);
        }

        public async Task AddPicture(PictureCreateDto dto)
        {

            Picture picture = dtoProvider.Mapper.Map<Picture>(dto);
            if (pictureRepo.ReadAll().FirstOrDefault(x => x.Title == picture.Title) == null)
            {
                pictureRepo.Create(picture);
            }
            else
            {
                throw new ArgumentException("Picture with the same name already exists");
            }
        }

        public async Task AddPictureList(PictureCreateDto dto)
        {

            Picture picture = dtoProvider.Mapper.Map<Picture>(dto);
            if (pictureRepo.ReadAll().FirstOrDefault(x => x.Title == picture.Title) == null)
            {
                pictureRepo.Create(picture);
            }
            else
            {
                throw new ArgumentException("Picture with the same name already exists");
            }
        }

        public IEnumerable<PictureShortViewDto> GetAllPictures()
        {
            return pictureRepo.ReadAll().Select(x => dtoProvider.Mapper.Map<PictureShortViewDto>(x))
                .Include(p => p.Owner)
                .ToList();
        }

        public void DeletePicture(string id)
        {
            pictureRepo.Remove(id);
        }

        public void DeleteOwnerPicture(string id, string userId)
        {
            var picture = pictureRepo.Read(id);
            if (picture.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this picture.");
            }
            pictureRepo.Remove(id);
        }

        public void UpdatePicture(string id, PictureCreateUpdateDto dto)
        {
            var oldPicture = pictureRepo.Read(id);
            dtoProvider.Mapper.Map(dto, oldPicture);
            pictureRepo.Update(oldPicture);
        }

        public void UpdatePictureAdmin(string id, PictureCreateUpdateDto dto)
        {
            var oldPicture = pictureRepo.Read(id);
            dtoProvider.Mapper.Map(dto, oldPicture);
            pictureRepo.Update(oldPicture);
        }

        public PictureViewDto GetPicture(string id)
        {
            var picture = pictureRepo.Read(id);
            return dtoProvider.Mapper.Map<PictureViewDto>(picture);
        }
        public void AddPicturesFromJson(string list)
        {
            var pictures = JsonSerializer.Deserialize<List<PictureCreateDto>>(list);
            foreach (var picture in pictures)
            {
                AddPictureList(picture).GetAwaiter().GetResult();
            }
        }


    }
}
