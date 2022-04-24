using src.SqlLiteReaderModel.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderModel.Services
{
    public class SqlLiteCareerInformation : ICareerInformation
    {
        private const string DATABASE_PILOT_TEMPLATE = "SELECT career.id, career.playerId, career.personageId, career.ironMan, career.state, career.insDate, pilot.name, pilot.lastName, squadron.airfield FROM career INNER JOIN pilot ON career.playerId == pilot.id INNER JOIN squadron ON career.id == squadron.careerId where career.isDeleted = 0 and career.ironMan != @Ironman";

        private readonly IDatabaseConnection databaseConnection;

        public SqlLiteCareerInformation(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Pilot[] GetPilots() => GetPilots(false);
        public Pilot[] GetPilots(bool showIronman)
        {
            Pilot[] pilots = new Pilot[0];
            if (databaseConnection == null)
            {
                return pilots;
            }
            using (DbConnection con = databaseConnection.GetConnection())
            {
                int ironManValue = showIronman ? 2 : 1;
                con.Open();
                DbCommand command = databaseConnection.CreateCommand(DATABASE_PILOT_TEMPLATE);
                databaseConnection.AddParameter(command, "@Ironman", ironManValue);
                command.Prepare();


                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        List<Pilot> databasePilots = new List<Pilot>();
                        while (reader.Read())
                        {
                            //Pilot pilot = new Pilot();
                            int pilotId = reader.GetInt32(1);
                            string guid = reader.GetString(2);
                            bool ironMan = reader.GetBoolean(3);
                            int state = reader.GetInt32(4);
                            string insertDate = reader.GetString(5);
                            DateTime insertionDate = DateTime.ParseExact(insertDate, "yyyy.MM.dd H:mm:ss", null);
                            string name = reader.GetString(6);
                            string lastName = reader.GetString(7);
                            string airField = reader.GetString(8);
                            databasePilots.Add(new Pilot(pilotId, guid, ironMan, state != 2, insertionDate, name, lastName, airField));
                        }
                        pilots = databasePilots.ToArray();
                    }
                }
            }
            return pilots;
        }

        public bool RevivePilot(Pilot pilotToRevive)
        {
            throw new NotImplementedException();
        }
    }
}