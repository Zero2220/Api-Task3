using Microsoft.EntityFrameworkCore;
using Services.Exceptions;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApi.Data;
using UniversityApi.Dtos;

namespace Services.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly UniDatabase _UniDatabase;
        public GroupService(UniDatabase uniDatabase) 
        {
             _UniDatabase = uniDatabase;
        }
        public int Create(CreateDto createDto)
        {
            if(createDto == null) throw new ArgumentNullException(nameof(createDto));
            
                if (_UniDatabase.Groups.Any(x => x.No == createDto.No))
                    throw new DublicateEntityException();

            Group group = new Group()
            {
                IsDeleted = false,
                Limit = createDto.Limit,
                No = createDto.No,
            };

            _UniDatabase.Groups.Add(group);
            _UniDatabase.SaveChanges();

            return group.Id;
        }

        public int Delete(int id)
        {
           if(id < 0) throw new InvalidIdEntityException();

           Group group = _UniDatabase.Groups.FirstOrDefault(x => x.Id == id);

            group.IsDeleted = true;

            _UniDatabase.Update(group);
            _UniDatabase.SaveChanges();

            return group.Id;
        }

        public int Edit(int id ,EditDto editDto)
        {

            if (editDto == null) throw new ArgumentNullException();

            if(editDto == null)throw new ArgumentNullException(nameof(editDto));

            var existGroup = _UniDatabase.Groups.FirstOrDefault(x=>x.Id == id);

            if(editDto.No == existGroup.No && _UniDatabase.Groups.Any(x => x.Id == id))
            throw new DublicateEntityException(editDto.No);
            

            existGroup.No = editDto.No;
            existGroup.Limit = editDto.Limit;
            

            _UniDatabase.Update(existGroup);
            _UniDatabase.SaveChanges();
            return existGroup.Id;
        }

        public List<GroupGetDto> GetAll()
        {
            List<Group> groups = _UniDatabase.Groups.ToList();

            List<GroupGetDto> groupsGetList = groups.Select(x => new GroupGetDto
            {
                No = x.No,
                Limit = x.Limit
            }).ToList();

            return groupsGetList;
        }

        public GroupGetDto GetById(int id)
        {
            var data = _UniDatabase.Groups.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                throw new NullReferenceException();
            }

            GroupGetDto dto = new GroupGetDto
            {
                No = data.No,
                Limit = data.Limit
            };
            return (dto);
        }
    }
}
