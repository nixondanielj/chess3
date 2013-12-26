using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using DataAccess.Model;
namespace DataAccess
{
    public class UserMapper : Mapper<User>
    {
        public override User Map(SqlDataReader reader)
        {
            var model = new User();
            model.Id = ToInt(reader["Id"]);
            model.Created = ToDateTime(reader["Created"]);
            model.Email = ToString(reader["Email"]);
            model.Password = ToString(reader["Password"]);
            return model;
        }

    }

    public class GameTypeMapper : Mapper<GameType>
    {
        public override GameType Map(SqlDataReader reader)
        {
            var model = new GameType();
            model.Id = ToInt(reader["Id"]);
            model.Active = ToBool(reader["Active"]);
            model.Name = ToString(reader["Name"]);
            return model;
        }

    }

    public class GameMapper : Mapper<Game>
    {
        public override Game Map(SqlDataReader reader)
        {
            var model = new Game();
            model.Id = ToInt(reader["Id"]);
            model.Type = ToInt(reader["Type"]);
            model.TurnPlayerId = ToInt(reader["TurnPlayerId"]);
            model.WinnerId = ToNullableInt(reader["WinnerId"]);
            model.Created = ToDateTime(reader["Created"]);
            model.Ended = ToNullableDateTime(reader["Ended"]);
            return model;
        }

    }

    public class MoveMapper : Mapper<Move>
    {
        public override Move Map(SqlDataReader reader)
        {
            var model = new Move();
            model.Id = ToInt(reader["Id"]);
            model.UserId = ToInt(reader["UserId"]);
            model.GameId = ToInt(reader["GameId"]);
            model.TimeMade = ToDateTime(reader["TimeMade"]);
            model.Value = ToString(reader["Value"]);
            return model;
        }

    }

    public class TokenMapper : Mapper<Token>
    {
        public override Token Map(SqlDataReader reader)
        {
            var model = new Token();
            model.UserId = ToInt(reader["UserId"]);
            model.Issued = ToDateTime(reader["Issued"]);
            model.Key = ToString(reader["Key"]);
            return model;
        }

    }

    public partial class UserRepository : Repository<User>
    {
        internal UserRepository(SqlConnection connection)
            : base(connection, new UserMapper())
        { }
        public virtual void Create(User model)
        {
            string text = "INSERT INTO Users ([Created], [Email], [Password]) VALUES(@Created, @Email, @Password)";
            ExecuteParamNonQuery(text, new SqlParameter("@Created", model.Created), new SqlParameter("@Email", model.Email ?? (object)DBNull.Value), new SqlParameter("@Password", model.Password ?? (object)DBNull.Value));
        }

        public virtual List<User> GetById(int val)
        {
            IEnumerable<User> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Created], t.[Email], t.[Password] FROM Users t WHERE t.Id = @Id", new SqlParameter("@Id", val));
            return results.ToList();
        }

        public virtual List<User> GetByCreated(DateTime val)
        {
            IEnumerable<User> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Created], t.[Email], t.[Password] FROM Users t WHERE t.Created = @Created", new SqlParameter("@Created", val));
            return results.ToList();
        }

        public virtual List<User> GetByEmail(string val)
        {
            IEnumerable<User> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Created], t.[Email], t.[Password] FROM Users t WHERE t.Email = @Email", new SqlParameter("@Email", val));
            return results.ToList();
        }

        public virtual List<User> GetByPassword(string val)
        {
            IEnumerable<User> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Created], t.[Email], t.[Password] FROM Users t WHERE t.Password = @Password", new SqlParameter("@Password", val));
            return results.ToList();
        }

        public virtual List<User> CustomGet(string where)
        {
            return ExecuteQuery("SELECT t.[Id], t.[Created], t.[Email], t.[Password] FROM Users t WHERE " + where).ToList();
        }

        public virtual IEnumerable<User> GetAll()
        {
            return ExecuteQuery("SELECT p.[Id], p.[Created], p.[Email], p.[Password] FROM Users p");
        }

        public virtual void Delete(int id)
        {
            ExecuteParamNonQuery("DELETE FROM Users WHERE Id = @Id", new SqlParameter("@Id", id));
        }

        public virtual void Delete(User model)
        {
            Delete(model.Id);
        }

        public virtual void Update(User model)
        {
            var sparams = new SqlParameter[4];
            sparams[0] = new SqlParameter("Created", model.Created);
            sparams[1] = new SqlParameter("Email", model.Email ?? (object)DBNull.Value);
            sparams[2] = new SqlParameter("Password", model.Password ?? (object)DBNull.Value);
            sparams[3] = new SqlParameter("@Id", model.Id);
            ExecuteParamNonQuery("UPDATE Users SET [Created] = @Created, [Email] = @Email, [Password] = @Password WHERE [Id] = @Id", sparams);
        }

        public virtual User GetByGame(Game model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Created], p.[Email], p.[Password] FROM Users p JOIN Games f ON p.Id = f.TurnPlayerId WHERE f.Id = @Id", new SqlParameter("@Id", model.Id)).SingleOrDefault();
        }

        public virtual User GetByGame(Game model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Created], p.[Email], p.[Password] FROM Users p JOIN Games f ON p.Id = f.TurnPlayerId WHERE f.Id = @Id", new SqlParameter("@Id", model.Id)).SingleOrDefault();
        }

        public virtual User GetByMove(Move model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Created], p.[Email], p.[Password] FROM Users p JOIN Moves f ON p.Id = f.UserId WHERE f.Id = @Id", new SqlParameter("@Id", model.Id)).SingleOrDefault();
        }

        public virtual User GetByToken(Token model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Created], p.[Email], p.[Password] FROM Users p JOIN Tokens f ON p.Id = f.UserId WHERE f.Key = @Key", new SqlParameter("@Key", model.Key)).SingleOrDefault();
        }

        public virtual List<User> GetByGame(Game model)
        {
            return ExecuteParamQuery("SELECT t.[Id], t.[Created], t.[Email], t.[Password] FROM Users t JOIN GamesUsers j ON t.Id = j.UserId WHERE j.GameId = @Id", new SqlParameter("@Id", model.Id)).ToList();
        }

        public virtual void AddRelationship(User primary, Game foreign)
        {
            ExecuteParamNonQuery("INSERT INTO GamesUsers ([GameId], [UserId]) VALUES (@Foreign, @Primary)", new SqlParameter("@Foreign", foreign.Id), new SqlParameter("@Primary", primary.Id));
        }

        public virtual void RemoveRelationship(User primary, Game foreign)
        {
            ExecuteParamNonQuery("DELETE FROM GamesUsers j WHERE j.UserId = @Primary AND j.GameId = @Foreign", new SqlParameter("@Primary", primary.Id), new SqlParameter("@Foreign", foreign.Id));
        }

    }

    public partial class GameTypeRepository : Repository<GameType>
    {
        internal GameTypeRepository(SqlConnection connection)
            : base(connection, new GameTypeMapper())
        { }
        public virtual void Create(GameType model)
        {
            string text = "INSERT INTO GameTypes ([Active], [Name]) VALUES(@Active, @Name)";
            ExecuteParamNonQuery(text, new SqlParameter("@Active", model.Active), new SqlParameter("@Name", model.Name ?? (object)DBNull.Value));
        }

        public virtual List<GameType> GetById(int val)
        {
            IEnumerable<GameType> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Active], t.[Name] FROM GameTypes t WHERE t.Id = @Id", new SqlParameter("@Id", val));
            return results.ToList();
        }

        public virtual List<GameType> GetByActive(bool val)
        {
            IEnumerable<GameType> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Active], t.[Name] FROM GameTypes t WHERE t.Active = @Active", new SqlParameter("@Active", val));
            return results.ToList();
        }

        public virtual List<GameType> GetByName(string val)
        {
            IEnumerable<GameType> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Active], t.[Name] FROM GameTypes t WHERE t.Name = @Name", new SqlParameter("@Name", val));
            return results.ToList();
        }

        public virtual List<GameType> CustomGet(string where)
        {
            return ExecuteQuery("SELECT t.[Id], t.[Active], t.[Name] FROM GameTypes t WHERE " + where).ToList();
        }

        public virtual IEnumerable<GameType> GetAll()
        {
            return ExecuteQuery("SELECT p.[Id], p.[Active], p.[Name] FROM GameTypes p");
        }

        public virtual void Delete(int id)
        {
            ExecuteParamNonQuery("DELETE FROM GameTypes WHERE Id = @Id", new SqlParameter("@Id", id));
        }

        public virtual void Delete(GameType model)
        {
            Delete(model.Id);
        }

        public virtual void Update(GameType model)
        {
            var sparams = new SqlParameter[3];
            sparams[0] = new SqlParameter("Active", model.Active);
            sparams[1] = new SqlParameter("Name", model.Name ?? (object)DBNull.Value);
            sparams[2] = new SqlParameter("@Id", model.Id);
            ExecuteParamNonQuery("UPDATE GameTypes SET [Active] = @Active, [Name] = @Name WHERE [Id] = @Id", sparams);
        }

        public virtual GameType GetByGame(Game model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Active], p.[Name] FROM GameTypes p JOIN Games f ON p.Id = f.Type WHERE f.Id = @Id", new SqlParameter("@Id", model.Id)).SingleOrDefault();
        }

    }

    public partial class GameRepository : Repository<Game>
    {
        internal GameRepository(SqlConnection connection)
            : base(connection, new GameMapper())
        { }
        public virtual void Create(Game model)
        {
            string text = "INSERT INTO Games ([Type], [TurnPlayerId], [WinnerId], [Created], [Ended]) VALUES(@Type, @TurnPlayerId, @WinnerId, @Created, @Ended)";
            ExecuteParamNonQuery(text, new SqlParameter("@Type", model.Type), new SqlParameter("@TurnPlayerId", model.TurnPlayerId), new SqlParameter("@WinnerId", model.WinnerId ?? (object)DBNull.Value), new SqlParameter("@Created", model.Created), new SqlParameter("@Ended", model.Ended ?? (object)DBNull.Value));
        }

        public virtual List<Game> GetById(int val)
        {
            IEnumerable<Game> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.Id = @Id", new SqlParameter("@Id", val));
            return results.ToList();
        }

        public virtual List<Game> GetByType(int val)
        {
            IEnumerable<Game> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.Type = @Type", new SqlParameter("@Type", val));
            return results.ToList();
        }

        public virtual List<Game> GetByTurnPlayerId(int val)
        {
            IEnumerable<Game> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.TurnPlayerId = @TurnPlayerId", new SqlParameter("@TurnPlayerId", val));
            return results.ToList();
        }

        public virtual List<Game> GetByWinnerId(int? val)
        {
            IEnumerable<Game> results;
            if (val == null)
            {
                results = ExecuteQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.WinnerId IS NULL");
            }

            else
            {
                results = ExecuteParamQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.WinnerId = @WinnerId", new SqlParameter("@WinnerId", val));
            }

            return results.ToList();
        }

        public virtual List<Game> GetByCreated(DateTime val)
        {
            IEnumerable<Game> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.Created = @Created", new SqlParameter("@Created", val));
            return results.ToList();
        }

        public virtual List<Game> GetByEnded(DateTime? val)
        {
            IEnumerable<Game> results;
            if (val == null)
            {
                results = ExecuteQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.Ended IS NULL");
            }

            else
            {
                results = ExecuteParamQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE t.Ended = @Ended", new SqlParameter("@Ended", val));
            }

            return results.ToList();
        }

        public virtual List<Game> CustomGet(string where)
        {
            return ExecuteQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t WHERE " + where).ToList();
        }

        public virtual IEnumerable<Game> GetAll()
        {
            return ExecuteQuery("SELECT p.[Id], p.[Type], p.[TurnPlayerId], p.[WinnerId], p.[Created], p.[Ended] FROM Games p");
        }

        public virtual void Delete(int id)
        {
            ExecuteParamNonQuery("DELETE FROM Games WHERE Id = @Id", new SqlParameter("@Id", id));
        }

        public virtual void Delete(Game model)
        {
            Delete(model.Id);
        }

        public virtual void Update(Game model)
        {
            var sparams = new SqlParameter[6];
            sparams[0] = new SqlParameter("Type", model.Type);
            sparams[1] = new SqlParameter("TurnPlayerId", model.TurnPlayerId);
            sparams[2] = new SqlParameter("WinnerId", model.WinnerId ?? (object)DBNull.Value);
            sparams[3] = new SqlParameter("Created", model.Created);
            sparams[4] = new SqlParameter("Ended", model.Ended ?? (object)DBNull.Value);
            sparams[5] = new SqlParameter("@Id", model.Id);
            ExecuteParamNonQuery("UPDATE Games SET [Type] = @Type, [TurnPlayerId] = @TurnPlayerId, [WinnerId] = @WinnerId, [Created] = @Created, [Ended] = @Ended WHERE [Id] = @Id", sparams);
        }

        public virtual List<Game> GetByUser(User model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Type], p.[TurnPlayerId], p.[WinnerId], p.[Created], p.[Ended] FROM Games p WHERE TurnPlayerId = @TurnPlayerId", new SqlParameter("@TurnPlayerId", model.Id)).ToList();
        }

        public virtual void SetRelationship(User primary, Game foreign)
        {
            ExecuteParamNonQuery("UPDATE Games SET TurnPlayerId = @Id WHERE Id = @Id", new SqlParameter("@Id", primary.Id), new SqlParameter("@Id", foreign.Id));
        }

        public virtual List<Game> GetByUser(User model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Type], p.[TurnPlayerId], p.[WinnerId], p.[Created], p.[Ended] FROM Games p WHERE TurnPlayerId = @TurnPlayerId", new SqlParameter("@TurnPlayerId", model.Id)).ToList();
        }

        public virtual void SetRelationship(User primary, Game foreign)
        {
            ExecuteParamNonQuery("UPDATE Games SET TurnPlayerId = @Id WHERE Id = @Id", new SqlParameter("@Id", primary.Id), new SqlParameter("@Id", foreign.Id));
        }

        public virtual List<Game> GetByGameType(GameType model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Type], p.[TurnPlayerId], p.[WinnerId], p.[Created], p.[Ended] FROM Games p WHERE Type = @Type", new SqlParameter("@Type", model.Id)).ToList();
        }

        public virtual void SetRelationship(GameType primary, Game foreign)
        {
            ExecuteParamNonQuery("UPDATE Games SET Type = @Id WHERE Id = @Id", new SqlParameter("@Id", primary.Id), new SqlParameter("@Id", foreign.Id));
        }

        public virtual Game GetByMove(Move model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[Type], p.[TurnPlayerId], p.[WinnerId], p.[Created], p.[Ended] FROM Games p JOIN Moves f ON p.Id = f.GameId WHERE f.Id = @Id", new SqlParameter("@Id", model.Id)).SingleOrDefault();
        }

        public virtual List<Game> GetByUser(User model)
        {
            return ExecuteParamQuery("SELECT t.[Id], t.[Type], t.[TurnPlayerId], t.[WinnerId], t.[Created], t.[Ended] FROM Games t JOIN GamesUsers j ON t.Id = j.GameId WHERE j.UserId = @Id", new SqlParameter("@Id", model.Id)).ToList();
        }

        public virtual void AddRelationship(Game primary, User foreign)
        {
            ExecuteParamNonQuery("INSERT INTO GamesUsers ([UserId], [GameId]) VALUES (@Foreign, @Primary)", new SqlParameter("@Foreign", foreign.Id), new SqlParameter("@Primary", primary.Id));
        }

        public virtual void RemoveRelationship(Game primary, User foreign)
        {
            ExecuteParamNonQuery("DELETE FROM GamesUsers j WHERE j.UserId = @Primary AND j.GameId = @Foreign", new SqlParameter("@Primary", primary.Id), new SqlParameter("@Foreign", foreign.Id));
        }

    }

    public partial class MoveRepository : Repository<Move>
    {
        internal MoveRepository(SqlConnection connection)
            : base(connection, new MoveMapper())
        { }
        public virtual void Create(Move model)
        {
            string text = "INSERT INTO Moves ([UserId], [GameId], [TimeMade], [Value]) VALUES(@UserId, @GameId, @TimeMade, @Value)";
            ExecuteParamNonQuery(text, new SqlParameter("@UserId", model.UserId), new SqlParameter("@GameId", model.GameId), new SqlParameter("@TimeMade", model.TimeMade), new SqlParameter("@Value", model.Value ?? (object)DBNull.Value));
        }

        public virtual List<Move> GetById(int val)
        {
            IEnumerable<Move> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[UserId], t.[GameId], t.[TimeMade], t.[Value] FROM Moves t WHERE t.Id = @Id", new SqlParameter("@Id", val));
            return results.ToList();
        }

        public virtual List<Move> GetByUserId(int val)
        {
            IEnumerable<Move> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[UserId], t.[GameId], t.[TimeMade], t.[Value] FROM Moves t WHERE t.UserId = @UserId", new SqlParameter("@UserId", val));
            return results.ToList();
        }

        public virtual List<Move> GetByGameId(int val)
        {
            IEnumerable<Move> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[UserId], t.[GameId], t.[TimeMade], t.[Value] FROM Moves t WHERE t.GameId = @GameId", new SqlParameter("@GameId", val));
            return results.ToList();
        }

        public virtual List<Move> GetByTimeMade(DateTime val)
        {
            IEnumerable<Move> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[UserId], t.[GameId], t.[TimeMade], t.[Value] FROM Moves t WHERE t.TimeMade = @TimeMade", new SqlParameter("@TimeMade", val));
            return results.ToList();
        }

        public virtual List<Move> GetByValue(string val)
        {
            IEnumerable<Move> results;
            results = ExecuteParamQuery("SELECT t.[Id], t.[UserId], t.[GameId], t.[TimeMade], t.[Value] FROM Moves t WHERE t.Value = @Value", new SqlParameter("@Value", val));
            return results.ToList();
        }

        public virtual List<Move> CustomGet(string where)
        {
            return ExecuteQuery("SELECT t.[Id], t.[UserId], t.[GameId], t.[TimeMade], t.[Value] FROM Moves t WHERE " + where).ToList();
        }

        public virtual IEnumerable<Move> GetAll()
        {
            return ExecuteQuery("SELECT p.[Id], p.[UserId], p.[GameId], p.[TimeMade], p.[Value] FROM Moves p");
        }

        public virtual void Delete(int id)
        {
            ExecuteParamNonQuery("DELETE FROM Moves WHERE Id = @Id", new SqlParameter("@Id", id));
        }

        public virtual void Delete(Move model)
        {
            Delete(model.Id);
        }

        public virtual void Update(Move model)
        {
            var sparams = new SqlParameter[5];
            sparams[0] = new SqlParameter("UserId", model.UserId);
            sparams[1] = new SqlParameter("GameId", model.GameId);
            sparams[2] = new SqlParameter("TimeMade", model.TimeMade);
            sparams[3] = new SqlParameter("Value", model.Value ?? (object)DBNull.Value);
            sparams[4] = new SqlParameter("@Id", model.Id);
            ExecuteParamNonQuery("UPDATE Moves SET [UserId] = @UserId, [GameId] = @GameId, [TimeMade] = @TimeMade, [Value] = @Value WHERE [Id] = @Id", sparams);
        }

        public virtual List<Move> GetByUser(User model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[UserId], p.[GameId], p.[TimeMade], p.[Value] FROM Moves p WHERE UserId = @UserId", new SqlParameter("@UserId", model.Id)).ToList();
        }

        public virtual void SetRelationship(User primary, Move foreign)
        {
            ExecuteParamNonQuery("UPDATE Moves SET UserId = @Id WHERE Id = @Id", new SqlParameter("@Id", primary.Id), new SqlParameter("@Id", foreign.Id));
        }

        public virtual List<Move> GetByGame(Game model)
        {
            return ExecuteParamQuery("SELECT p.[Id], p.[UserId], p.[GameId], p.[TimeMade], p.[Value] FROM Moves p WHERE GameId = @GameId", new SqlParameter("@GameId", model.Id)).ToList();
        }

        public virtual void SetRelationship(Game primary, Move foreign)
        {
            ExecuteParamNonQuery("UPDATE Moves SET GameId = @Id WHERE Id = @Id", new SqlParameter("@Id", primary.Id), new SqlParameter("@Id", foreign.Id));
        }

    }

    public partial class TokenRepository : Repository<Token>
    {
        internal TokenRepository(SqlConnection connection)
            : base(connection, new TokenMapper())
        { }
        public virtual void Create(Token model)
        {
            string text = "INSERT INTO Tokens ([UserId], [Issued]) VALUES(@UserId, @Issued)";
            ExecuteParamNonQuery(text, new SqlParameter("@UserId", model.UserId), new SqlParameter("@Issued", model.Issued));
        }

        public virtual List<Token> GetByUserId(int val)
        {
            IEnumerable<Token> results;
            results = ExecuteParamQuery("SELECT t.[UserId], t.[Issued], t.[Key] FROM Tokens t WHERE t.UserId = @UserId", new SqlParameter("@UserId", val));
            return results.ToList();
        }

        public virtual List<Token> GetByIssued(DateTime val)
        {
            IEnumerable<Token> results;
            results = ExecuteParamQuery("SELECT t.[UserId], t.[Issued], t.[Key] FROM Tokens t WHERE t.Issued = @Issued", new SqlParameter("@Issued", val));
            return results.ToList();
        }

        public virtual List<Token> GetByKey(string val)
        {
            IEnumerable<Token> results;
            results = ExecuteParamQuery("SELECT t.[UserId], t.[Issued], t.[Key] FROM Tokens t WHERE t.Key = @Key", new SqlParameter("@Key", val));
            return results.ToList();
        }

        public virtual List<Token> CustomGet(string where)
        {
            return ExecuteQuery("SELECT t.[UserId], t.[Issued], t.[Key] FROM Tokens t WHERE " + where).ToList();
        }

        public virtual IEnumerable<Token> GetAll()
        {
            return ExecuteQuery("SELECT p.[UserId], p.[Issued], p.[Key] FROM Tokens p");
        }

        public virtual void Delete(string id)
        {
            ExecuteParamNonQuery("DELETE FROM Tokens WHERE Key = @Key", new SqlParameter("@Key", id));
        }

        public virtual void Delete(Token model)
        {
            Delete(model.Key);
        }

        public virtual void Update(Token model)
        {
            var sparams = new SqlParameter[3];
            sparams[0] = new SqlParameter("UserId", model.UserId);
            sparams[1] = new SqlParameter("Issued", model.Issued);
            sparams[2] = new SqlParameter("@Key", model.Key);
            ExecuteParamNonQuery("UPDATE Tokens SET [UserId] = @UserId, [Issued] = @Issued WHERE [Key] = @Key", sparams);
        }

        public virtual List<Token> GetByUser(User model)
        {
            return ExecuteParamQuery("SELECT p.[UserId], p.[Issued], p.[Key] FROM Tokens p WHERE UserId = @UserId", new SqlParameter("@UserId", model.Id)).ToList();
        }

        public virtual void SetRelationship(User primary, Token foreign)
        {
            ExecuteParamNonQuery("UPDATE Tokens SET UserId = @Id WHERE Key = @Key", new SqlParameter("@Id", primary.Id), new SqlParameter("@Key", foreign.Key));
        }

    }


    public abstract partial class Repository<T>
    {
        private SqlConnection db;

        private IMapper<T> _mapper;

        protected Repository(SqlConnection connection, IMapper<T> mapper)
        {
            db = connection;
            _mapper = mapper; // some kind of factory based on typeof(T) to not force the subclass to pass this in?
        }

        partial void LogCmd(SqlCommand command);

        protected void ExecuteParamNonQuery(string command, params SqlParameter[] parameters)
        {
            using (var cmd = BuildCommand(command, false))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                LogCmd(cmd);
                cmd.ExecuteNonQuery();
            }
        }

        protected IEnumerable<T> ExecuteParamQuery(string command, params SqlParameter[] parameters)
        {
            using (var cmd = BuildCommand(command, false))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                return ExecuteReader(cmd);
            }
        }

        protected IEnumerable<T> ExecuteSproc(string procName, params SqlParameter[] sqlParameters)
        {
            using (var cmd = BuildCommand(procName, true))
            {
                if (sqlParameters != null)
                {
                    foreach (var parameter in sqlParameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                return ExecuteReader(cmd);
            }
        }

        /// <summary>
        /// Ensure that values are in order of appearance in the query. THIS IS NOT FOR STORED PROCS. USE EXECUTESPROC.
        /// </summary>
        /// <param name="cmd">The text of the query</param>
        /// <param name="parameterValues">The parameter values in order of appearance</param>
        /// <returns>Query results (T)</returns>
        protected IEnumerable<T> ExecuteParamQuery(string cmd, params object[] parameterValues)
        {
            var parameterNames = GetParameterNames(cmd);
            if (parameterNames.Count == parameterValues.Length)
            {
                using (var command = BuildCommand(cmd, false))
                {
                    for (int i = 0; i < parameterNames.Count; i++)
                    {
                        command.Parameters.Add(new SqlParameter(parameterNames[i], parameterValues[i]));
                    }
                    return ExecuteReader(command);
                }
            }
            else
            {
                throw new Exception("Ensure that there is a parameter value provided for each parameter in the query");
            }
        }

        private List<string> GetParameterNames(string cmd)
        {
            var matches = Regex.Matches(cmd, @"@[\w$#]*");
            List<string> parameters = new List<string>();
            foreach (var match in matches)
            {
                if (!parameters.Contains(match.ToString()))
                {
                    parameters.Add(match.ToString());
                }
            }
            return parameters;
        }

        protected IEnumerable<T> ExecuteQuery(string cmd)
        {
            return ExecuteQuery(cmd, _mapper.Map);
        }

        protected IEnumerable<TD> ExecuteQuery<TD>(string cmd, Func<SqlDataReader, TD> map)
        {
            using (SqlCommand command = BuildCommand(cmd, false))
            {
                return ExecuteReader(command, map);
            }
        }

        protected IEnumerable<T> ExecuteReader(SqlCommand cmd)
        {
            return ExecuteReader(cmd, _mapper.Map);
        }

        protected IEnumerable<TD> ExecuteReader<TD>(SqlCommand cmd, Func<SqlDataReader, TD> map)
        {
            LogCmd(cmd);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return map(reader);
                }
            }
        }

        protected void ExecuteNonQuery(string cmd)
        {
            using (SqlCommand command = BuildCommand(cmd, false))
            {
                LogCmd(command);
                command.ExecuteNonQuery();
            }
        }

        private SqlCommand BuildCommand(string cmd, bool sproc)
        {
            var command = new SqlCommand(cmd, db);
            if (sproc)
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
            }
            return command;
        }
    }



    public interface IMapper<T>
    {
        T Map(SqlDataReader reader);
    }

    public abstract class Mapper<T> : IMapper<T>
    {
        public abstract T Map(SqlDataReader reader);

        private TD ToType<TD>(object value, Func<string, TD> parse)
        {
            return parse(value.ToString());
        }

        private Nullable<TD> ToNullableType<TD>(object value, Func<string, TD> parse) where TD : struct
        {
            Nullable<TD> result = null;
            if (value != DBNull.Value)
            {
                result = ToType(value, parse);
            }
            return result;
        }

        protected DateTime? ToNullableDateTime(object value)
        {
            return ToNullableType(value, DateTime.Parse);
        }

        protected decimal? ToNullableDecimal(object value)
        {
            return ToNullableType(value, Decimal.Parse);
        }

        protected TimeSpan ToTimeSpan(object value)
        {
            return ToType(value, TimeSpan.Parse);
        }

        protected TimeSpan? ToNullableTimeSpan(object value)
        {
            return ToNullableType(value, TimeSpan.Parse);
        }

        protected DateTime ToDateTime(object value)
        {
            return ToType(value, DateTime.Parse);
        }

        protected int? ToNullableInt(object value)
        {
            return ToNullableType(value, Int32.Parse);
        }

        protected int ToInt(object value)
        {
            return ToType(value, Int32.Parse);
        }

        protected string ToString(object value)
        {
            string result = null;
            if (value != DBNull.Value)
            {
                result = value.ToString();
            }
            return result;
        }

        protected bool ToBool(object value)
        {
            return ToType(value, bool.Parse);
        }

        protected bool? ToNullableBool(object value)
        {
            return ToNullableType(value, bool.Parse);
        }

        protected double ToDouble(object value)
        {
            return ToType(value, double.Parse);
        }

        protected decimal ToDecimal(object value)
        {
            return ToType(value, decimal.Parse);
        }
    }
    public class UnitOfWork : IDisposable
    {
        protected SqlConnection connection;
        public UnitOfWork()
        {
            connection = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = GameDB; Integrated Security = SSPI;");
            connection.Open();
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public virtual UserRepository UserRepository
        {
            get
            {
                return new UserRepository(connection);
            }

        }

        public virtual GameTypeRepository GameTypeRepository
        {
            get
            {
                return new GameTypeRepository(connection);
            }

        }

        public virtual GameRepository GameRepository
        {
            get
            {
                return new GameRepository(connection);
            }

        }

        public virtual MoveRepository MoveRepository
        {
            get
            {
                return new MoveRepository(connection);
            }

        }

        public virtual TokenRepository TokenRepository
        {
            get
            {
                return new TokenRepository(connection);
            }

        }

    }

}

