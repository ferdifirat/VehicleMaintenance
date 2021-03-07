using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Entity.Concrete
{
    public class PictureGroup : BaseEntity, IEntity
    {
        public string PictureImage { get; set; }
    }
}
