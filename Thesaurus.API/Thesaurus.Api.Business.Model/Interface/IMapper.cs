using System;
using System.Collections.Generic;
using System.Text;

namespace Thesaurus.Api.Business.Model.Interface
{
    public interface IMapper<Tin,Tout>
    {
        Tout Map(Tin source);
    }
}
