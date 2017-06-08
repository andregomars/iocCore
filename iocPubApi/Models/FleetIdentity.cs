namespace iocPubApi.Models
{
    public class FleetIdentity
    {
        public string Fname { get; set; }
        public string VehicleType { get; set; }
        public string Icon { get; set; }

        public bool Equals(FleetIdentity other)
        {
            if (string.Equals(Fname, other.Fname) &&
                string.Equals(VehicleType, other.VehicleType) &&
                string.Equals(Icon, other.Icon))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            int hashFname = Fname == null ? 0 : Fname.GetHashCode();
            int hashVehicleType = VehicleType == null ? 0 : VehicleType.GetHashCode();
            int hashIcon = Icon == null ? 0 : Icon.GetHashCode();

            return hashFname ^ hashVehicleType ^ hashIcon;
        }
    }
}