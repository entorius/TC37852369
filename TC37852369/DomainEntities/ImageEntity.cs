using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class ImageEntity
    {
        public long id { set; get; }
        public string link { set; get; }
        public string entityId { set; get; }
        public string entityName { set; get; }
        public long imageNumber { set; get; }
        public ImageEntity(long id, string link, string entityId,string entityName, long imageNumber)
        {
            this.id = id;
            this.link = link;
            this.entityId = entityId;
            this.entityName = entityName;
            this.imageNumber = imageNumber;
        }
    }
}
