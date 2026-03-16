// Represents the top-level GeoJSON object returned by the USGS API
public class FeatureCollection
{
    public List<Feature> Features { get; set; } = [];
}

// Represents a single earthquake event
public class Feature
{
    public EarthquakeProperties Properties { get; set; } = new();
}

// The properties we care about on each earthquake feature
public class EarthquakeProperties
{
    public string Place { get; set; } = string.Empty;
    public double? Mag   { get; set; }
}