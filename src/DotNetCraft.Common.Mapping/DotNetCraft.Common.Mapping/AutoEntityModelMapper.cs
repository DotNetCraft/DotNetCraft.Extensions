using System;
using System.Collections.Generic;
using AutoMapper;
using DotNetCraft.Common.Core.DataAccessLayer;

namespace DotNetCraft.Common.Mapping
{
    public class AutoEntityModelMapper: IEntityModelMapper
    {
        private readonly IMapper mapper;

        public AutoEntityModelMapper(IMapper mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            this.mapper = mapper;
        }

        #region Implementation of IEntityModelMapper

        public TModel Map<TEntity, TModel>(TEntity entity)
        {
            return mapper.Map<TModel>(entity);
        }

        public TEntity Map<TEntity, TModel>(TModel model)
        {
            return mapper.Map<TEntity>(model);
        }

        public List<TModel> Map<TEntity, TModel>(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> Map<TEntity, TModel>(ICollection<TModel> models)
        {
            return mapper.Map<List<TEntity>>(models);
        }

        #endregion
    }
}
