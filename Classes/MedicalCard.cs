//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.Classes
{
    using System;
    using System.Collections.Generic;
    
    public partial class MedicalCard
    {
        public int IDMedicalBook { get; set; }
        public Nullable<int> IDDoctor { get; set; }
        public Nullable<int> IDPatient { get; set; }
        public Nullable<int> IDDiagnosis { get; set; }
    
        public virtual Diagnosis Diagnosis { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
