using RHApplicationLib.Abstract;

namespace ClientApplication.Data
{
    public class DBikeHeartBeat : DAbstract
    {
        public ushort HeartBeat { get; set; }
    }

    public class DBikeGeneralFEData : DAbstract
    {
        public byte EquipmentTypeField { get; set; }

        /// <summary>
        /// Elapsed time since start workout (unit: 0.25 seconds). Rollover at 64 seconds
        /// </summary>
        public byte ElapsedTime { get; set; }

        /// <summary>
        /// Distance traveled since start of workout (unit: meters). Rollover at 256 meters.
        /// </summary>
        public byte DistanceTraveled { get; set; }

        /// <summary>
        /// invalid value: 0xFFFF, unit: 0.001 m/s
        /// </summary>
        public ushort Speed { get; set; }

        /// <summary>
        /// invalid value: 0xFF, unit: bpm
        /// </summary>
        public byte HeartRate { get; set; }

        public byte CapabilitiesField { get; set; }

        public byte FEStateField { get; set; }
    }

    public class DBikeSpecificBikeData : DAbstract
    {
        public byte UpdateEventCount { get; set; }

        /// <summary>
        /// invalid value: 0xFF, unit: RPM
        /// </summary>
        public byte Cadence { get; set; }

        /// <summary>
        /// unit: Watt (accumulated). Rollover at 65536 Watt
        /// </summary>
        public ushort PowerGenerated { get; set; }

        /// <summary>
        ///  invalid value: 0xFFF (also means that powerGenerated is invalid!), unit: Watts (generated now).
        /// </summary>
        public ushort Power { get; set; }

        public byte TrainerStatusField { get; set; }

        public byte FlagsField { get; set; }

        public byte FEStateField { get; set; }
    }
}
