﻿using AutoMapper;
using System.Linq.Expressions;

using api.Repositories.Interfaces;
using api.Services.Interfaces.Base;


namespace api.Services
{
    public class BaseService<TEntity, TDto> : IBaseService<TDto>
		where TEntity : class
		where TDto : class
	{
		protected readonly IBaseCRUDRepository<TEntity> _repo;
		protected readonly IMapper _mapper;


		public BaseService(IBaseCRUDRepository<TEntity> repository, IMapper mapper)
		{
			_repo = repository;
			_mapper = mapper;
		}



		public virtual async Task<TDto> Create(TDto dto)
		{
			TEntity persistedEntity = await _repo.Create(_mapper.Map<TEntity>(dto));
			TDto newDto = _mapper.Map<TDto>(persistedEntity);
			return newDto;
		}


		public virtual async Task Delete(TDto dto)
			=> await _repo.Delete(_mapper.Map<TEntity>(dto));


		public virtual async Task UpdateWith(TDto dto)
			=> await _repo.Update(_mapper.Map<TEntity>(dto));
		
		
		protected virtual async Task<IEnumerable<TDto>> Get(Expression<Func<TEntity, bool>>? filter = null, bool trackEntities = false, string? includeField = null)
			=> (await _repo.ReadMany(filter, trackEntities, includeField)).Select(e => _mapper.Map<TDto>(e));
	}
}
