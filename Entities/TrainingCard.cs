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
    
    public partial class TrainingCard
    {
        public long TrainingID { get; set; }
        public int FlashcardID { get; set; }
        public int InternalLossCount { get; set; }
    
        public virtual Flashcard Flashcard { get; set; }
        public virtual TrainingSession TrainingSession { get; set; }
    }
}
