using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public unsafe partial struct ctl_device_adapter_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public void* pDeviceID;

        [NativeTypeName("uint32_t")]
        public uint device_id_size;

        public ctl_device_type_t device_type;

        [NativeTypeName("ctl_supported_functions_flags_t")]
        public uint supported_subfunction_flags;

        [NativeTypeName("uint64_t")]
        public ulong driver_version;

        public ctl_firmware_version_t firmware_version;

        [NativeTypeName("uint32_t")]
        public uint pci_vendor_id;

        [NativeTypeName("uint32_t")]
        public uint pci_device_id;

        [NativeTypeName("uint32_t")]
        public uint rev_id;

        [NativeTypeName("uint32_t")]
        public uint num_eus_per_sub_slice;

        [NativeTypeName("uint32_t")]
        public uint num_sub_slices_per_slice;

        [NativeTypeName("uint32_t")]
        public uint num_slices;

        [NativeTypeName("char[100]")]
        public _name_e__FixedBuffer name;

        [NativeTypeName("ctl_adapter_properties_flags_t")]
        public uint graphics_adapter_properties;

        [NativeTypeName("uint32_t")]
        public uint Frequency;

        [NativeTypeName("uint16_t")]
        public ushort pci_subsys_id;

        [NativeTypeName("uint16_t")]
        public ushort pci_subsys_vendor_id;

        public ctl_adapter_bdf_t adapter_bdf;

        [NativeTypeName("uint32_t")]
        public uint num_xe_cores;

        [NativeTypeName("char[108]")]
        public _reserved_e__FixedBuffer reserved;

        [InlineArray(100)]
        public partial struct _name_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(108)]
        public partial struct _reserved_e__FixedBuffer
        {
            public sbyte e0;
        }
    }
}
