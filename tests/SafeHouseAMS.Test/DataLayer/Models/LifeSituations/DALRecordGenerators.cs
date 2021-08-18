using System.Collections.Generic;
using System.Linq;
using FsCheck;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

namespace SafeHouseAMS.Test.DataLayer.Models.LifeSituations
{
    internal class DALRecordGenerators
    {
        private static readonly Gen<BaseRecordDAL> ChildrenGen = Gen.Constant(new ChildrenRecordDAL() as BaseRecordDAL);
        private static readonly Gen<BaseRecordDAL> CitiGen = Gen.Constant(new CitizenshipRecordDAL() as BaseRecordDAL);
        private static readonly Gen<BaseRecordDAL> DomicileGen = Gen.Constant(new DomicileRecordDAL() as BaseRecordDAL);
        private static readonly Gen<BaseRecordDAL> EduGen = Gen.Constant(new EducationLevelRecordDAL() as BaseRecordDAL);
        private static readonly Gen<BaseRecordDAL> SpecGen = Gen.Constant(new SpecialityRecordDAL() as BaseRecordDAL);
        private static readonly Gen<BaseRecordDAL> MigGen = Gen.Constant(new MigrationStatusRecordDAL() as BaseRecordDAL);
        private static readonly Gen<BaseRecordDAL> RegGen = Gen.Constant(new RegistrationStatusRecordDAL() as BaseRecordDAL);

        public static Arbitrary<IEnumerable<BaseRecordDAL>> RecordListGen => Arb.From(
        Gen
            .OneOf(ChildrenGen, CitiGen, DomicileGen, EduGen, MigGen, RegGen, SpecGen)
            .ListOf()
            .Select(x => x.AsEnumerable())
        );
    }
}