using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using com.github.mreutegg.laszip4j;
using com.github.mreutegg.laszip4j.laslib;

public static class LASheaderExtensions
{
    public static LASHeaderData ConvertLASHeader(this LASHeader header)
    {
        if (header == null)
        {
            return null; // Or throw an exception
        }

        var data = new LASHeaderData
        {
            FileSignature = header.getFileSignature(), // [cite: 9, 10]
            FileSourceID = header.getFileSourceID(), // [cite: 11, 12]
            GlobalEncoding = header.getGlobalEncoding(), // [cite: 13, 14]
            ProjectID_GUIDData1 = header.getProjectID_GUIDData1(), // [cite: 15, 16, 17, 18, 19, 20, 21, 22, 23]
            ProjectID_GUIDData2 = header.getProjectID_GUIDData2(),
            ProjectID_GUIDData3 = header.getProjectID_GUIDData3(),
            ProjectID_GUIDData4 = header.getProjectID_GUIDData4(),
            VersionMajor = header.getVersionMajor(), // [cite: 24, 25, 26, 27, 28, 29, 30]
            VersionMinor = header.getVersionMinor(),
            SystemIdentifier = header.getSystemIdentifier(),
            GeneratingSoftware = header.getGeneratingSoftware(),
            FileCreationDayOfYear = header.getFileCreationDayOfYear(), // [cite: 31, 32, 33, 34, 35, 36, 37, 38, 39, 40]
            FileCreationYear = header.getFileCreationYear(),
            HeaderSize = header.getHeaderSize(),
            OffsetToPointData = header.getOffsetToPointData(),
            NumberOfVariableLengthRecords = header.getNumberOfVariableLengthRecords(),
            PointDataRecordFormat = header.getPointDataRecordFormat(), // [cite: 41, 42, 43, 44, 45, 46, 47, 48, 49]
            PointDataRecordLength = header.getPointDataRecordLength(),
            LegacyNumberOfPointRecords = header.getLegacyNumberOfPointRecords(),
            LegacyNumberOfPointsByReturn = header.getLegacyNumberOfPointsByReturn(),
            XScaleFactor = header.getXScaleFactor(), // [cite: 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72]
            YScaleFactor = header.getYScaleFactor(),
            ZScaleFactor = header.getZScaleFactor(),
            XOffset = header.getXOffset(),
            YOffset = header.getYOffset(),
            ZOffset = header.getZOffset(),
            MaxX = header.getMaxX(),
            MinX = header.getMinX(),
            MaxY = header.getMaxY(),
            MinY = header.getMinY(),
            MaxZ = header.getMaxZ(),
            MinZ = header.getMinZ(),
            StartOfWaveformDataPacketRecord = header.getStartOfWaveformDataPacketRecord(), // [cite: 73, 74, 75, 76, 77, 78, 79, 80, 81, 82]
            StartOfFirstExtendedVariableLengthRecord = header.getStartOfFirstExtendedVariableLengthRecord(),
            NumberOfExtendedVariableLengthRecords = header.getNumberOfExtendedVariableLengthRecords(),
            NumberOfPointRecords = header.getNumberOfPointRecords(),
            NumberOfPointsByReturn = header.getNumberOfPointsByReturn(),
            VariableLengthRecords = ConvertIterableToList<LASVariableLengthRecord>(header.getVariableLengthRecords()).AsEnumerable(),
            ExtendedVariableLengthRecords = ConvertIterableToList<LASExtendedVariableLengthRecord>(header.getExtendedVariableLengthRecords()).AsEnumerable(),
            ExtraBytesDescriptions = ConvertIterableToList<LASExtraBytesDescription>(header.getExtraBytesDescriptions()).AsEnumerable()

        };
        

        return data;
    }
    public static LASheaderData ConvertLASHeader(this LASheader header)
    {
        if (header == null)
        {
            return null; // Or throw an exception
        }

        var data = new LASheaderData
        {
            FileSignature = stringFromByteArray(header.file_signature), // [cite: 9, 10]
            FileSourceID = header.file_source_ID, // [cite: 11, 12]
            GlobalEncoding = header.global_encoding, // [cite: 13, 14]
            ProjectID_GUIDData1 = header.project_ID_GUID_data_1, // [cite: 15, 16, 17, 18, 19, 20, 21, 22, 23]
            ProjectID_GUIDData2 = header.project_ID_GUID_data_2,
            ProjectID_GUIDData3 = header.project_ID_GUID_data_3,
            ProjectID_GUIDData4 = header.project_ID_GUID_data_4,
            VersionMajor = header.version_major, // [cite: 24, 25, 26, 27, 28, 29, 30]
            VersionMinor = header.version_minor,
            SystemIdentifier = stringFromByteArray(header.system_identifier),
            GeneratingSoftware = stringFromByteArray(header.generating_software),
            FileCreationDayOfYear = header.file_creation_day, // [cite: 31, 32, 33, 34, 35, 36, 37, 38, 39, 40]
            FileCreationYear = header.file_creation_year,
            HeaderSize = header.header_size,
            OffsetToPointData = header.offset_to_point_data,
            NumberOfVariableLengthRecords = header.number_of_variable_length_records,
            PointDataRecordFormat = header.point_data_format, // [cite: 41, 42, 43, 44, 45, 46, 47, 48, 49]
            PointDataRecordLength = header.point_data_record_length,
            XScaleFactor = header.x_scale_factor, // [cite: 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72]
            YScaleFactor = header.y_scale_factor,
            ZScaleFactor = header.z_scale_factor,
            XOffset = header.x_offset,
            YOffset = header.y_offset,
            ZOffset = header.z_offset,
            MaxX = header.max_x,
            MinX = header.min_x,
            MaxY = header.max_y,
            MinY = header.min_y,
            MaxZ = header.max_z,
            MinZ = header.min_z,
            StartOfWaveformDataPacketRecord = header.start_of_waveform_data_packet_record, // [cite: 73, 74, 75, 76, 77, 78, 79, 80, 81, 82]
            StartOfFirstExtendedVariableLengthRecord = header.start_of_first_extended_variable_length_record,
            NumberOfExtendedVariableLengthRecords = header.number_of_extended_variable_length_records,
            NumberOfPointRecords = header.number_of_point_records,
            NumberOfPointsByReturn = header.number_of_points_by_return,

        };


        return data;
    }

    public static List<T> ConvertIterableToList<T>(java.lang.Iterable iterable)
    {
        var list = new List<T>();
        var iterator = iterable.iterator();
        while (iterator.hasNext())
        {
            list.Add((T)iterator.next());
        }
        return list;
    }
    public class LASHeaderData
    {
        public string FileSignature { get; set; }
        public char FileSourceID { get; set; }
        public char GlobalEncoding { get; set; }
        public int ProjectID_GUIDData1 { get; set; }
        public char ProjectID_GUIDData2 { get; set; }
        public char ProjectID_GUIDData3 { get; set; }
        public byte[] ProjectID_GUIDData4 { get; set; }
        public byte VersionMajor { get; set; }
        public byte VersionMinor { get; set; }
        public string SystemIdentifier { get; set; }
        public string GeneratingSoftware { get; set; }
        public char FileCreationDayOfYear { get; set; }
        public char FileCreationYear { get; set; }
        public char HeaderSize { get; set; }
        public int OffsetToPointData { get; set; }
        public int NumberOfVariableLengthRecords { get; set; }
        public byte PointDataRecordFormat { get; set; }
        public char PointDataRecordLength { get; set; }
        public int LegacyNumberOfPointRecords { get; set; }
        public int[] LegacyNumberOfPointsByReturn { get; set; }
        public double XScaleFactor { get; set; }
        public double YScaleFactor { get; set; }
        public double ZScaleFactor { get; set; }
        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public double ZOffset { get; set; }
        public double MaxX { get; set; }
        public double MinX { get; set; }
        public double MaxY { get; set; }
        public double MinY { get; set; }
        public double MaxZ { get; set; }
        public double MinZ { get; set; }
        public long StartOfWaveformDataPacketRecord { get; set; }
        public long StartOfFirstExtendedVariableLengthRecord { get; set; }
        public int NumberOfExtendedVariableLengthRecords { get; set; }
        public long NumberOfPointRecords { get; set; }
        public long[] NumberOfPointsByReturn { get; set; }
        public IEnumerable<LASVariableLengthRecord> VariableLengthRecords { get; set; }
        public IEnumerable<LASExtendedVariableLengthRecord> ExtendedVariableLengthRecords { get; set; }
        public IEnumerable<LASExtraBytesDescription> ExtraBytesDescriptions { get; set; }

    }
    public class LASheaderData
    {
        public string FileSignature { get; set; }
        public char FileSourceID { get; set; }
        public char GlobalEncoding { get; set; }
        public int ProjectID_GUIDData1 { get; set; }
        public char ProjectID_GUIDData2 { get; set; }
        public char ProjectID_GUIDData3 { get; set; }
        public byte[] ProjectID_GUIDData4 { get; set; }
        public byte VersionMajor { get; set; }
        public byte VersionMinor { get; set; }
        public string SystemIdentifier { get; set; }
        public string GeneratingSoftware { get; set; }
        public char FileCreationDayOfYear { get; set; }
        public char FileCreationYear { get; set; }
        public char HeaderSize { get; set; }
        public int OffsetToPointData { get; set; }
        public int NumberOfVariableLengthRecords { get; set; }
        public byte PointDataRecordFormat { get; set; }
        public char PointDataRecordLength { get; set; }
        public double XScaleFactor { get; set; }
        public double YScaleFactor { get; set; }
        public double ZScaleFactor { get; set; }
        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public double ZOffset { get; set; }
        public double MaxX { get; set; }
        public double MinX { get; set; }
        public double MaxY { get; set; }
        public double MinY { get; set; }
        public double MaxZ { get; set; }
        public double MinZ { get; set; }
        public long StartOfWaveformDataPacketRecord { get; set; }
        public long StartOfFirstExtendedVariableLengthRecord { get; set; }
        public int NumberOfExtendedVariableLengthRecords { get; set; }
        public long NumberOfPointRecords { get; set; }
        public int[] NumberOfPointsByReturn { get; set; }

    }
    public static string ToFormattedString(this LASHeaderData data)
    {
        if (data == null)
        {
            return "LASHeaderData object is null.";
        }

        var sb = new StringBuilder();

        sb.AppendLine("LASHeaderData:");
        sb.AppendLine("--------------");

        sb.AppendLine($"File Signature: {data.FileSignature}");

        // Handle char fields explicitly (casting to int)
        sb.AppendLine($"File Source ID: {(int)data.FileSourceID}");
        sb.AppendLine($"Global Encoding: {(int)data.GlobalEncoding}");
        sb.AppendLine($"Project ID - GUID Data 2: {(int)data.ProjectID_GUIDData2}");
        sb.AppendLine($"Project ID - GUID Data 3: {(int)data.ProjectID_GUIDData3}");
        sb.AppendLine($"File Creation Day Of Year: {(int)data.FileCreationDayOfYear}");
        sb.AppendLine($"File Creation Year: {(int)data.FileCreationYear}");
        sb.AppendLine($"Header Size: {(int)data.HeaderSize}");
        sb.AppendLine($"Point Data Record Length: {(int)data.PointDataRecordLength}");

        // Handle byte
        sb.AppendLine($"Project ID - GUID Data 4: {BitConverter.ToString(data.ProjectID_GUIDData4).Replace("-", ", ")}");

        sb.AppendLine($"Version Major: {data.VersionMajor}");
        sb.AppendLine($"Version Minor: {data.VersionMinor}");
        sb.AppendLine($"System Identifier: {data.SystemIdentifier}");
        sb.AppendLine($"Generating Software: {data.GeneratingSoftware}");
        sb.AppendLine($"Offset To Point Data: {data.OffsetToPointData}");
        sb.AppendLine($"Number Of Variable Length Records: {data.NumberOfVariableLengthRecords}");
        sb.AppendLine($"Point Data Record Format: {data.PointDataRecordFormat}");
        sb.AppendLine($"Legacy Number Of Point Records: {data.LegacyNumberOfPointRecords}");
        sb.AppendLine($"X Scale Factor: {data.XScaleFactor}");
        sb.AppendLine($"Y Scale Factor: {data.YScaleFactor}");
        sb.AppendLine($"Z Scale Factor: {data.ZScaleFactor}");
        sb.AppendLine($"X Offset: {data.XOffset}");
        sb.AppendLine($"Y Offset: {data.YOffset}");
        sb.AppendLine($"Z Offset: {data.ZOffset}");
        sb.AppendLine($"Max X: {data.MaxX}");
        sb.AppendLine($"Min X: {data.MinX}");
        sb.AppendLine($"Max Y: {data.MaxY}");
        sb.AppendLine($"Min Y: {data.MinY}");
        sb.AppendLine($"Max Z: {data.MaxZ}");
        sb.AppendLine($"Min Z: {data.MinZ}");
        sb.AppendLine($"Start Of Waveform Data Packet Record: {data.StartOfWaveformDataPacketRecord}");
        sb.AppendLine($"Start Of First Extended Variable Length Record: {data.StartOfFirstExtendedVariableLengthRecord}");
        sb.AppendLine($"Number Of Extended Variable Length Records: {data.NumberOfExtendedVariableLengthRecords}");
        sb.AppendLine($"Number Of Point Records: {data.NumberOfPointRecords}");
        sb.AppendLine($"Number Of Points By Return: {string.Join(", ", data.NumberOfPointsByReturn)}");
        sb.AppendLine($"Legacy Number Of Points By Return: {string.Join(", ", data.LegacyNumberOfPointsByReturn)}");

        // Handle IEnumerable<T> properties
        sb.AppendLine("Variable Length Records:");
        if (data.VariableLengthRecords != null && data.VariableLengthRecords.Any())
        {
            foreach (var record in data.VariableLengthRecords)
            {
                sb.AppendLine($"  - {record}"); // Or format the record as needed
            }
        }
        else
        {
            sb.AppendLine("  - None");
        }

        sb.AppendLine("Extended Variable Length Records:");
        if (data.ExtendedVariableLengthRecords != null && data.ExtendedVariableLengthRecords.Any())
        {
            foreach (var record in data.ExtendedVariableLengthRecords)
            {
                sb.AppendLine($"  - {record}"); // Or format the record as needed
            }
        }
        else
        {
            sb.AppendLine("  - None");
        }

        sb.AppendLine("Extra Bytes Descriptions:");
        if (data.ExtraBytesDescriptions != null && data.ExtraBytesDescriptions.Any())
        {
            foreach (var description in data.ExtraBytesDescriptions)
            {
                sb.AppendLine($"  - {description}"); // Or format the description as needed
            }
        }
        else
        {
            sb.AppendLine("  - None");
        }

        return sb.ToString();
    }
    public static string ToFormattedString(this LASheaderData data)
        {
            if (data == null)
            {
                return "LASHeaderData object is null.";
            }

            var sb = new StringBuilder();

            sb.AppendLine("LASHeaderData:");
            sb.AppendLine("--------------");

            sb.AppendLine($"File Signature: {data.FileSignature}");

            // Handle char fields explicitly
            sb.AppendLine($"File Source ID: {(int)data.FileSourceID}"); // Cast to int to see the numeric value
            sb.AppendLine($"Global Encoding: {(int)data.GlobalEncoding}");
            sb.AppendLine($"Project ID - GUID Data 2: {(int)data.ProjectID_GUIDData2}");
            sb.AppendLine($"Project ID - GUID Data 3: {(int)data.ProjectID_GUIDData3}");
            sb.AppendLine($"File Creation Day Of Year: {(int)data.FileCreationDayOfYear}");
            sb.AppendLine($"File Creation Year: {(int)data.FileCreationYear}");
            sb.AppendLine($"Header Size: {(int)data.HeaderSize}");
            sb.AppendLine($"Point Data Record Length: {(int)data.PointDataRecordLength}");

            // Handle byte
            sb.AppendLine($"Project ID - GUID Data 4: {BitConverter.ToString(data.ProjectID_GUIDData4).Replace("-", ", ")}");

            sb.AppendLine($"Version Major: {data.VersionMajor}");
            sb.AppendLine($"Version Minor: {data.VersionMinor}");
            sb.AppendLine($"System Identifier: {data.SystemIdentifier}");
            sb.AppendLine($"Generating Software: {data.GeneratingSoftware}");
            sb.AppendLine($"Offset To Point Data: {data.OffsetToPointData}");
            sb.AppendLine($"Number Of Variable Length Records: {data.NumberOfVariableLengthRecords}");
            sb.AppendLine($"Point Data Record Format: {data.PointDataRecordFormat}");
            sb.AppendLine($"X Scale Factor: {data.XScaleFactor}");
            sb.AppendLine($"Y Scale Factor: {data.YScaleFactor}");
            sb.AppendLine($"Z Scale Factor: {data.ZScaleFactor}");
            sb.AppendLine($"X Offset: {data.XOffset}");
            sb.AppendLine($"Y Offset: {data.YOffset}");
            sb.AppendLine($"Z Offset: {data.ZOffset}");
            sb.AppendLine($"Max X: {data.MaxX}");
            sb.AppendLine($"Min X: {data.MinX}");
            sb.AppendLine($"Max Y: {data.MaxY}");
            sb.AppendLine($"Min Y: {data.MinY}");
            sb.AppendLine($"Max Z: {data.MaxZ}");
            sb.AppendLine($"Min Z: {data.MinZ}");
            sb.AppendLine($"Start Of Waveform Data Packet Record: {data.StartOfWaveformDataPacketRecord}");
            sb.AppendLine($"Start Of First Extended Variable Length Record: {data.StartOfFirstExtendedVariableLengthRecord}");
            sb.AppendLine($"Number Of Extended Variable Length Records: {data.NumberOfExtendedVariableLengthRecords}");
            sb.AppendLine($"Number Of Point Records: {data.NumberOfPointRecords}");
            sb.AppendLine($"Number Of Points By Return: {string.Join(", ", data.NumberOfPointsByReturn)}");

            return sb.ToString();
        }
    private static string stringFromByteArray(byte[] bytes)
    {
        return com.github.mreutegg.laszip4j.laszip.MyDefs.stringFromByteArray(bytes);
    }
    public static string ToCsvString(this List<GeoPoint> geoPoints)
    {
        if (geoPoints == null)
        {
            return ""; // Or throw an exception if you prefer
        }

        var sb = new StringBuilder();

        // Add the header row
        sb.AppendLine("X,Y,Z,NIR,Classification,Intensity,ReturnNumber1,NumberOfReturns,DateTime");

        foreach (var point in geoPoints)
        {
            // Format each row
            sb.AppendLine($"{point.X},{point.Y},{point.Z},{point.NIR},{point.Classification},{point.Intensity},{point.ReturnNumber1},{point.NumberOfReturns},{point.Datetime}");
        }

        return sb.ToString();
    }

}
public class GeoPoint // All Lidar points
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double NIR { get; set; }
    public int Classification { get; set; }
    public double Intensity { get; set; }
    public double ReturnNumber1 { get; set; }
    public double NumberOfReturns { get; set; }
    public string Datetime { get; set; }


}