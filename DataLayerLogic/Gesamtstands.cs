using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataLayerLogic
{
    /// <summary>
    /// Frage 1: was ist, wenn Ergebnis nicht verfügbar?
    /// Frage 2: hidden games
    ///     /// 
    /// Check on alle Spiele vorbei: pro Runde => done
    /// 
    /// admin (WebSite admin Tippeingabe): DB Einträge nur dann, wenn alle Spiele vorbei sind (Config Minuten nach letztem Spiel => siehe Aufüllen von nicht getippt)
    /// admin: methode "adminInputManagement" pro Round

    /// user: 1. Check on DB-Eintrag => done
    /// user: 2. wenn alle SPiele vorbei sind && kein DB Eintrag => Dann Berechnung und DB-Eintrag (Config Minuten nach letztem Spiel => siehe Aufüllen von nicht getippt) => done
    /// user: methode insert => done
    /// </summary>
    public class Gesamtstands
    {
        
        private int _richtigeTipps;
        public int richtigeTipps
        { 
            get{return _richtigeTipps;}
            set {_richtigeTipps = value ;}
        }
        private int _falscheTipps;
        public int falscheTipps
        {
            get { return _falscheTipps; }
            set { _falscheTipps = value; }
        }

        private int _echteBank;
        public int echteBank
        {
            get { return _echteBank; }
            set { _echteBank = value; }
        }

        private int _unechteBank;
        public int unechteBank
        {
            get { return _unechteBank; }
            set { _unechteBank = value; }
        }

        private int _nichtgetippt;
        public int nichtgetippt
        {
            get { return _nichtgetippt; }
            set { _nichtgetippt = value; }
        }

        private int _NeunerTipp;
        public int NeunerTipp
        {
            get { return _NeunerTipp; }
            set { _NeunerTipp = value; }
        }

        private int _PunkteInsgesamt;
        public int PunkteInsgesamt
        {
            get { return _PunkteInsgesamt; }
            set { _PunkteInsgesamt = value; }
        }

        private SqlConnection m_conn;
        private readonly Guid _roundId;
        private readonly Guid _memberId;
        
        private Gesamtstands(Guid roundId, Guid memberId)
        {
            _memberId = memberId;
            _roundId = roundId;
        }
        
        public Gesamtstands(SqlConnection conn, Guid roundId, Guid memberId) : this(roundId,memberId)
        {
            m_conn = conn;
        }

        
        /// <summary>
        /// ausgelöst vom admin wenn nach Abschluß des Spieltags Tipp eingetragen wurde
        /// alle Datensätze des Spieltags werden aus GesamtstandPerRound entfernt
        /// faulerweise auch über alle UserGroups
        /// </summary>
        internal void deleteGesamtstandOfRound()
        {
            string sqlStat = String.Format("delete from [GesamtstandPerRound] " +
                    "where spieltagid = '{0}'", _roundId);
            
            using (SqlConnection conn = new SqlConnection(m_conn.ConnectionString))
            {
                try
                {
                    SqlCommand commdel = new SqlCommand(sqlStat, conn);
                    conn.Open();
                    int anz = commdel.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void updateGesamtstandRow()
        {
            using (SqlConnection conn = new SqlConnection(m_conn.ConnectionString))
            {
                try
                {
                    SqlCommand commUpdate = new SqlCommand("UpdateGesamtstandPerRound", m_conn);
                    commUpdate.CommandType = CommandType.StoredProcedure;

                    commUpdate.Parameters.Add("@spieltagid", SqlDbType.UniqueIdentifier).Value = _roundId;
                    commUpdate.Parameters.Add("@memberid", SqlDbType.UniqueIdentifier).Value = _memberId;
                    commUpdate.Parameters.Add("@richtigeTipps", SqlDbType.Int).Value = _richtigeTipps;
                    commUpdate.Parameters.Add("@falscheTipps", SqlDbType.Int).Value = _falscheTipps;
                    commUpdate.Parameters.Add("@echteBank", SqlDbType.Int).Value = _echteBank;
                    commUpdate.Parameters.Add("@unechteBank", SqlDbType.Int).Value = _unechteBank;
                    commUpdate.Parameters.Add("@nichtgetippt", SqlDbType.Int).Value = _nichtgetippt;
                    commUpdate.Parameters.Add("@NeunerTipp", SqlDbType.Int).Value = _NeunerTipp;
                    commUpdate.Parameters.Add("@PunkteInsgesamt", SqlDbType.Int).Value = _PunkteInsgesamt;
                
                    m_conn.Open();
                    commUpdate.ExecuteNonQuery();
                }       
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    m_conn.Close();
                }
            }
        }

        internal void insertGesamtstandRow()
        {
            ////string insert = String.Format("INSERT INTO [GesamtstandPerRound] " +
            ////       "([id],[spieltagid],[memberid],[RichtigeTipps],[FalscheTipps],[EchteBank],[UnechteBank],[NichtGetippt],[NeunerTipp], [PunkteInsgesamt]) " +
            ////       "VALUES " +
            ////       "(newID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')"
            ////       , _roundId, _memberId, _richtigeTipps, _falscheTipps, _echteBank, _unechteBank, _nichtgetippt, _NeunerTipp, _PunkteInsgesamt);

            using (SqlConnection conn = new SqlConnection(m_conn.ConnectionString))
            {
                try
                {
                    SqlCommand commInsert = new SqlCommand("InsertGesamtstandPerRound", conn);
                    commInsert.CommandType = CommandType.StoredProcedure;

                    commInsert.Parameters.Add("@spieltagid", SqlDbType.UniqueIdentifier).Value = _roundId;
                    commInsert.Parameters.Add("@memberid", SqlDbType.UniqueIdentifier).Value = _memberId;
                    commInsert.Parameters.Add("@richtigeTipps", SqlDbType.Int).Value = _richtigeTipps;
                    commInsert.Parameters.Add("@falscheTipps", SqlDbType.Int).Value = _falscheTipps;
                    commInsert.Parameters.Add("@echteBank", SqlDbType.Int).Value = _echteBank;
                    commInsert.Parameters.Add("@unechteBank", SqlDbType.Int).Value = _unechteBank;
                    commInsert.Parameters.Add("@nichtgetippt", SqlDbType.Int).Value = _nichtgetippt;
                    commInsert.Parameters.Add("@NeunerTipp", SqlDbType.Int).Value = _NeunerTipp;
                    commInsert.Parameters.Add("@PunkteInsgesamt", SqlDbType.Int).Value = _PunkteInsgesamt;

                    conn.Open();

                    commInsert.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>are records in DB</returns>
        internal bool selectGesamtstandRound()
        {
            //string select = string.Format("select [id],[spieltagid],[memberid],[richtigetipps],[falschetipps],[echtebank] " +
            //                        ",[unechtebank] ,[nichtgetippt],[neunertipp], [punkteinsgesamt] " +
            //                        "from [gesamtstandperround] " +
            //                        "where [spieltagid] = '{0}' and [memberid] = '{1}'", _roundId, _memberId);

            string select = "select * from GetGesamtstandPerUser(@SpieltagId,@MemberId)";

            bool rowexists = false;

            using (SqlConnection conn = new SqlConnection(m_conn.ConnectionString))
            {

                try
                {
                    conn.Open();
                    int rowindex = 0;

                    SqlCommand commselect = new SqlCommand(select, conn);
                    commselect.Parameters.Add("@SpieltagId", SqlDbType.UniqueIdentifier).Value = _roundId;
                    commselect.Parameters.Add("@MemberId", SqlDbType.UniqueIdentifier).Value = _memberId;

                    SqlDataReader reader = commselect.ExecuteReader();

                    while (reader.Read())
                    {
                        rowindex++;
                        //Gesamtstands g = new Gesamtstands(reader.GetGuid(1), reader.GetGuid(2));
                        this.richtigeTipps = reader.GetInt32(3);
                        this.falscheTipps = reader.GetInt32(4);
                        this.echteBank = reader.GetInt32(5);
                        this.unechteBank = reader.GetInt32(6);
                        this.nichtgetippt = reader.GetInt32(7);
                        this.NeunerTipp = reader.GetInt32(8);
                        this.PunkteInsgesamt = reader.GetInt32(9);
                    }

                    reader.Close();

                    if (rowindex > 1)
                        throw new Exception("too many rows are returned from gesamtstandtable");

                    if (rowindex == 0)
                        rowexists = false;
                    else
                        rowexists = true;
                }
                catch (Exception ex)
                {
                    rowexists = false;
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

            return rowexists;
        }
    }
}
