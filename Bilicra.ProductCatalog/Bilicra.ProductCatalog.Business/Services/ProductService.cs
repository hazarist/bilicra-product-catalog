using AutoMapper;
using Bilicra.ProductCatalog.Business.Helpers;
using Bilicra.ProductCatalog.Business.Interfaces;
using Bilicra.ProductCatalog.Common.Entities;
using Bilicra.ProductCatalog.Common.Exceptions;
using Bilicra.ProductCatalog.Common.Models.ProductModels;
using Bilicra.ProductCatalog.DataAccess.Repository;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Business.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepository<ProductEntity> repository;
        private readonly IMapper mapper;
        public ProductService(IRepository<ProductEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Product not found");
            }
            var model = mapper.Map<ProductModel>(entity);
            return model;
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            var entityList = await repository.GetAllAsync();
            var model = mapper.Map<List<ProductModel>>(entityList);
            return model;
        }

        public async Task<List<ProductModel>> GetAllAsync(string name)
        {
            var entityList = await repository.GetAllAsync(x => x.Name.Contains(name));
            var model = mapper.Map<List<ProductModel>>(entityList);
            return model;
        }

        public async Task<IWorkbook> ExportExcelAsync()
        {
            var entityList = await repository.GetAllAsync();
            var modelList = mapper.Map<List<ProductModel>>(entityList);
            return ExcelHelper.WriteExcelWithNPOI(modelList);
        }

        public async Task<ProductModel> CreateAsync(ProductPostModel product)
        {
            var isCodeExist = await IsCodeExist(product.Code);
            if (isCodeExist)
            {
                throw new BadRequestException("Code is not unique please enter a diffrent code");
            }

            if (product.Price <= 0)
            {
                throw new BadRequestException("Price should be greater then 0");
            }

            var entity = mapper.Map<ProductEntity>(product);

            if (product.Price < 999)
            {
                entity.IsConfirmed = true;
            }

            await repository.CreateAsync(entity);
            var model = mapper.Map<ProductModel>(entity);
            return model;
        }

        public async Task<bool> IsCodeExist(string code)
        {
            var codeList = await repository.GetAllAsync();
            return codeList.Any(x => x.Code == code);
        }


        public async Task<ProductModel> UploadPhotoAsync(Guid productId,ProductPostPhotoModel photo)
        {
            var entity = await repository.GetByIdAsync(productId);
            if (entity == null)
            {
                throw new NotFoundException("Product not found");
            }

            if(photo.Photo == null || photo.Photo.Length  == 0)
            {
                throw new BadRequestException("Please select a file");
            }

            if (photo.Photo.Length > 1274000)
            {
                throw new BadRequestException("The file size is too much please upload an image under 1MB");
            }

            using (var ms = new MemoryStream())
            {
                photo.Photo.CopyTo(ms);
                var fileBytes = ms.ToArray();
                entity.Photo = Convert.ToBase64String(fileBytes);
                await repository.UpdateAsync(entity);
                var model = mapper.Map<ProductModel>(entity);
                return model;
            }
        }

        public async Task<ProductModel> UpdateAsync(ProductPutModel product)
        {
            var entity = await repository.GetByIdAsync(product.Id);
            if (entity == null)
            {
                throw new NotFoundException("Product not found");
            }
            var updatedEntity = mapper.Map(product, entity);
            await repository.UpdateAsync(updatedEntity);
            var model = mapper.Map<ProductModel>(updatedEntity);
            return model;
        }

        public async Task<ProductModel> DeleteAsync(Guid id)
        {
            var entity = await repository.DeleteAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Product not found");
            }
            var model = mapper.Map<ProductModel>(entity);
            return model;
        }

        public async Task<ProductModel> PriceConfirmPatchAsync(Guid id)
        {
            var entity = await repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException("Product not found");
            }

            entity.IsConfirmed = true;
            await repository.UpdateAsync(entity);
            var model = mapper.Map<ProductModel>(entity);
            return model;
        }

    }
}
