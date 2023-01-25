using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("pilot")]
[Index("Id", "IsDeleted", Name = "idx_pilot_id_isdeleted")]
[Index("SquadronId", "IsDeleted", Name = "idx_pilot_squadronid_isdeleted")]
public partial class Pilot
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("squadronId", TypeName = "INTEGER(11)")]
    public Squadron? Squadron { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("lastName", TypeName = "varchar(255)")]
    public string LastName { get; set; } = null!;

    [Column("birthDay", TypeName = "datetime")]
    public byte[]? BirthDay { get; set; }

    [Column("description", TypeName = "varchar(512)")]
    public string Description { get; set; } = null!;

    [Column("commonStat", TypeName = "INTEGER(4)")]
    public long CommonStat { get; set; }

    [Column("personageId", TypeName = "varchar(37)")]
    public string PersonageId { get; set; } = null!;

    [Column("avatarPath", TypeName = "varchar(255)")]
    public string AvatarPath { get; set; } = null!;

    [Column("AILevel", TypeName = "INTEGER(11)")]
    public long Ailevel { get; set; }

    [Column("score", TypeName = "INTEGER(11)")]
    public long Score { get; set; }

    [Column("pcp", TypeName = "float")]
    public double Pcp { get; set; }

    [Column("wounded", TypeName = "INTEGER(11)")]
    public long Wounded { get; set; }

    [Column("sorties", TypeName = "INTEGER(11)")]
    public long Sorties { get; set; }

    [Column("goodSorties", TypeName = "INTEGER(11)")]
    public long GoodSorties { get; set; }

    [Column("flightTime", TypeName = "INTEGER(11)")]
    public long FlightTime { get; set; }

    [Column("vehPot", TypeName = "INTEGER(11)")]
    public long VehPot { get; set; }

    [Column("shipPot", TypeName = "INTEGER(11)")]
    public long ShipPot { get; set; }

    [Column("trainPot", TypeName = "INTEGER(11)")]
    public long TrainPot { get; set; }

    [Column("buildingPot", TypeName = "INTEGER(11)")]
    public long BuildingPot { get; set; }

    [Column("penalty", TypeName = "varchar(256)")]
    public string Penalty { get; set; } = null!;

    [Column("transferProb", TypeName = "float")]
    public double TransferProb { get; set; }

    [Column("penaltyPot", TypeName = "INTEGER(11)")]
    public long PenaltyPot { get; set; }

    [Column("rankId", TypeName = "INTEGER(11)")]
    public long RankId { get; set; }

    [Column("state", TypeName = "INTEGER(11)")]
    public long State { get; set; }

    [Column("statePeriod", TypeName = "INTEGER(11)")]
    public long StatePeriod { get; set; }

    [Column("stateDate", TypeName = "datetime")]
    public byte[]? StateDate { get; set; }

    [Column("careerStartDate", TypeName = "datetime")]
    public byte[]? CareerStartDate { get; set; }

    [Column("killLightPlane", TypeName = "INTEGER(11)")]
    public long KillLightPlane { get; set; }

    [Column("killLightFighter", TypeName = "INTEGER(11)")]
    public long KillLightFighter { get; set; }

    [Column("killLightAttackPlane", TypeName = "INTEGER(11)")]
    public long KillLightAttackPlane { get; set; }

    [Column("killLightBomber", TypeName = "INTEGER(11)")]
    public long KillLightBomber { get; set; }

    [Column("killLightRecon", TypeName = "INTEGER(11)")]
    public long KillLightRecon { get; set; }

    [Column("killLightTransport", TypeName = "INTEGER(11)")]
    public long KillLightTransport { get; set; }

    [Column("killMediumPlane", TypeName = "INTEGER(11)")]
    public long KillMediumPlane { get; set; }

    [Column("killMediumFighter", TypeName = "INTEGER(11)")]
    public long KillMediumFighter { get; set; }

    [Column("killMediumAttackPlane", TypeName = "INTEGER(11)")]
    public long KillMediumAttackPlane { get; set; }

    [Column("killMediumBomber", TypeName = "INTEGER(11)")]
    public long KillMediumBomber { get; set; }

    [Column("killMediumRecon", TypeName = "INTEGER(11)")]
    public long KillMediumRecon { get; set; }

    [Column("killMediumTransport", TypeName = "INTEGER(11)")]
    public long KillMediumTransport { get; set; }

    [Column("killHeavyPlane", TypeName = "INTEGER(11)")]
    public long KillHeavyPlane { get; set; }

    [Column("killHeavyFighter", TypeName = "INTEGER(11)")]
    public long KillHeavyFighter { get; set; }

    [Column("killHeavyAttackPlane", TypeName = "INTEGER(11)")]
    public long KillHeavyAttackPlane { get; set; }

    [Column("killHeavyBomber", TypeName = "INTEGER(11)")]
    public long KillHeavyBomber { get; set; }

    [Column("killHeavyRecon", TypeName = "INTEGER(11)")]
    public long KillHeavyRecon { get; set; }

    [Column("killHeavyTransport", TypeName = "INTEGER(11)")]
    public long KillHeavyTransport { get; set; }

    [Column("killHeavyArmoured", TypeName = "INTEGER(11)")]
    public long KillHeavyArmoured { get; set; }

    [Column("killHeavyTank", TypeName = "INTEGER(11)")]
    public long KillHeavyTank { get; set; }

    [Column("killMediumTank", TypeName = "INTEGER(11)")]
    public long KillMediumTank { get; set; }

    [Column("killVehicle", TypeName = "INTEGER(11)")]
    public long KillVehicle { get; set; }

    [Column("killLightTank", TypeName = "INTEGER(11)")]
    public long KillLightTank { get; set; }

    [Column("killArmouredVehicle", TypeName = "INTEGER(11)")]
    public long KillArmouredVehicle { get; set; }

    [Column("killTruck", TypeName = "INTEGER(11)")]
    public long KillTruck { get; set; }

    [Column("killCar", TypeName = "INTEGER(11)")]
    public long KillCar { get; set; }

    [Column("killTrainLocomotive", TypeName = "INTEGER(11)")]
    public long KillTrainLocomotive { get; set; }

    [Column("killTrainVagon", TypeName = "INTEGER(11)")]
    public long KillTrainVagon { get; set; }

    [Column("killHowitzer", TypeName = "INTEGER(11)")]
    public long KillHowitzer { get; set; }

    [Column("killFieldGun", TypeName = "INTEGER(11)")]
    public long KillFieldGun { get; set; }

    [Column("killNavalGun", TypeName = "INTEGER(11)")]
    public long KillNavalGun { get; set; }

    [Column("killRocketLauncher", TypeName = "INTEGER(11)")]
    public long KillRocketLauncher { get; set; }

    [Column("killMachineGun", TypeName = "INTEGER(11)")]
    public long KillMachineGun { get; set; }

    [Column("killSearchlight", TypeName = "INTEGER(11)")]
    public long KillSearchlight { get; set; }

    [Column("killStaticPlane", TypeName = "INTEGER(11)")]
    public long KillStaticPlane { get; set; }

    [Column("killAirDefence", TypeName = "INTEGER(11)")]
    public long KillAirDefence { get; set; }

    [Column("killHeavyFlak", TypeName = "INTEGER(11)")]
    public long KillHeavyFlak { get; set; }

    [Column("killLightFlak", TypeName = "INTEGER(11)")]
    public long KillLightFlak { get; set; }

    [Column("killAAAMachineGun", TypeName = "INTEGER(11)")]
    public long KillAaamachineGun { get; set; }

    [Column("killShips", TypeName = "INTEGER(11)")]
    public long KillShips { get; set; }

    [Column("killLightShip", TypeName = "INTEGER(11)")]
    public long KillLightShip { get; set; }

    [Column("killDestroyerShip", TypeName = "INTEGER(11)")]
    public long KillDestroyerShip { get; set; }

    [Column("killSubmarine", TypeName = "INTEGER(11)")]
    public long KillSubmarine { get; set; }

    [Column("killLargeCargoShip", TypeName = "INTEGER(11)")]
    public long KillLargeCargoShip { get; set; }

    [Column("killBuilding", TypeName = "INTEGER(11)")]
    public long KillBuilding { get; set; }

    [Column("killRuralYard", TypeName = "INTEGER(11)")]
    public long KillRuralYard { get; set; }

    [Column("killTownBuilding", TypeName = "INTEGER(11)")]
    public long KillTownBuilding { get; set; }

    [Column("killFactoryBuilding", TypeName = "INTEGER(11)")]
    public long KillFactoryBuilding { get; set; }

    [Column("killRailwayStationFacility", TypeName = "INTEGER(11)")]
    public long KillRailwayStationFacility { get; set; }

    [Column("killBridge", TypeName = "INTEGER(11)")]
    public long KillBridge { get; set; }

    [Column("killAirfieldFacility", TypeName = "INTEGER(11)")]
    public long KillAirfieldFacility { get; set; }

    [Column("killAirCrew", TypeName = "INTEGER(11)")]
    public long KillAirCrew { get; set; }

    [Column("killPilot", TypeName = "INTEGER(11)")]
    public long KillPilot { get; set; }

    [Column("killPlaneGunner", TypeName = "INTEGER(11)")]
    public long KillPlaneGunner { get; set; }

    [Column("killDriver", TypeName = "INTEGER(11)")]
    public long KillDriver { get; set; }

    [Column("killVehicleGunner", TypeName = "INTEGER(11)")]
    public long KillVehicleGunner { get; set; }

    [Column("killInfantry", TypeName = "INTEGER(11)")]
    public long KillInfantry { get; set; }

    [Column("killTurrets", TypeName = "INTEGER(11)")]
    public long KillTurrets { get; set; }

    [Column("killPlaneTurrets", TypeName = "INTEGER(11)")]
    public long KillPlaneTurrets { get; set; }

    [Column("killVehicleTurrets", TypeName = "INTEGER(11)")]
    public long KillVehicleTurrets { get; set; }

    [Column("killPlaneInGroup", TypeName = "INTEGER(11)")]
    public long KillPlaneInGroup { get; set; }

    [Column("killAssist", TypeName = "INTEGER(11)")]
    public long KillAssist { get; set; }

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }
}
