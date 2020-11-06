using UnityEngine;

namespace Utility
{
    /// <summary>
    /// A struct used to describe spherical cooridinates.
    /// This is implicitly convertable with Vector3
    /// </summary>
    [System.Serializable]
    public struct SphericalPoint
    {
        public float Radius { get { return _radius; } set { _radius = value; } }
        public float Polar { get { return _polar; } set { _polar = value; } }
        public float Elevation { get { return _elevation; } set { _elevation = value; } }

        #region EditorProperties
#if UNITY_EDITOR

        public static string RADIUS_PROPERTY = "_radius";
        public static string POLAR_PROPERTY = "_polar";
        public static string ELEVATION_PROPERTY = "_elevation";

#endif
        #endregion

        [SerializeField]
        private float _radius;
        [SerializeField]
        private float _polar;
        [SerializeField]
        private float _elevation;

        public SphericalPoint(float radius, float polar, float elevation)
        {
            _radius = radius;
            _polar = polar;
            _elevation = elevation;
        }

        public SphericalPoint(Vector3 vector3)
        {
            this = Convert(vector3);
        }

        public static implicit operator Vector3(SphericalPoint point)
        {
            return Convert(point);
        }

        public static implicit operator SphericalPoint(Vector3 point)
        {
            return Convert(point);
        }

        public static Vector3 Convert(SphericalPoint point)
        {
            Vector3 output = new Vector3();
            float plane = point.Radius * Mathf.Cos(Mathf.Deg2Rad * point.Elevation);
            output.x = plane * Mathf.Cos(Mathf.Deg2Rad * (point.Polar - 90));
            output.y = point.Radius * Mathf.Sin(Mathf.Deg2Rad * point.Elevation);
            output.z = plane * Mathf.Sin(Mathf.Deg2Rad * (point.Polar - 90));
            return output;
        }

        public static SphericalPoint Convert(Vector3 point)
        {
            SphericalPoint output = new SphericalPoint();
            if (point.x == 0)
            {
                point.x = Mathf.Epsilon;
            }
            output.Radius = Mathf.Sqrt((point.x * point.x) + (point.y * point.y) + (point.z * point.z));
            output.Polar = Mathf.Atan(point.z / point.x);
            if (point.x < 0)
            {
                output.Polar += Mathf.PI;
            }
            output.Polar = (Mathf.Rad2Deg * output.Polar)-90;
            output.Elevation = Mathf.Rad2Deg * Mathf.Asin(point.y / output.Radius);
            return output;
        }

        public bool Equals(SphericalPoint other)
        {
            return Radius == other.Radius && Polar == other.Polar && Elevation == other.Elevation;
        }

        public override string ToString()
        {
            return string.Format("Radius: {0} Polar: {1} Elevation: {2}", Radius, Polar, Elevation);
        }

        public override bool Equals(object obj)
        {
            if (obj is SphericalPoint)
            {
                return Equals((SphericalPoint)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = ((Radius.GetHashCode() << 5) + Radius.GetHashCode()) ^ Polar.GetHashCode();
            hash = ((hash << 5) + hash) ^ Elevation.GetHashCode();
            return hash;
        }
    }

}