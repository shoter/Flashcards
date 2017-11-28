//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flashcards.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class FlashcardTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FlashcardTranslation()
        {
            this.FlashcardImages = new HashSet<FlashcardImage>();
        }
    
        public long ID { get; set; }
        public int FlashcardID { get; set; }
        public int LanguageID { get; set; }
        public Nullable<long> SpecificImageFileID { get; set; }
        public string Translation { get; set; }
        public string Pronounciation { get; set; }
        public decimal Significance { get; set; }
    
        public virtual FlashcardImage FlashcardImage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FlashcardImage> FlashcardImages { get; set; }
        public virtual Flashcard Flashcard { get; set; }
        public virtual Language Language { get; set; }
    }
}