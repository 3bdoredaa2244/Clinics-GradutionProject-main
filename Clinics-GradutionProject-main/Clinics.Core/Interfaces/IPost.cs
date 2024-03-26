using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinics.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinics.Core.Interfaces
{
    public interface IPost : IGenericRepository<Posts>
    {

    }
}
