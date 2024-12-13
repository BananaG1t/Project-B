using Microsoft.Data.Sqlite;

using Dapper;

public static class LocationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "Location";

    public static Int64 Write(LocationModel location)
    {
        string sql = $"INSERT INTO {Table} (name) VALUES (@Name)";
        _connection.Execute(sql, location);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }
    public static void Update(LocationModel snack)
    {
        string sql = $"UPDATE {Table} SET name = @Name WHERE id = @Id";
        _connection.Execute(sql, snack);
    }

    public static LocationModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<LocationModel>(sql, new { Id = id });
    }

    public static List<LocationModel> GetAll()
    {
        string sql = $"SELECT id, name FROM {Table}";
        List<LocationModel> Locations = (List<LocationModel>)_connection.Query<LocationModel>(sql);

        return Locations;
    }

    public static List<LocationModel> GetAllLocationsWithNoSchedules()
    {
        string sql = @$"SELECT DISTINCT Location.* FROM {Table} LEFT JOIN Schedule ON {Table}.id = Schedule.Location_ID WHERE Schedule.Location_ID IS NULL";
        List<LocationModel> locations = (List<LocationModel>)_connection.Query<LocationModel>(sql);

        return locations;
    }

    public static List<string> GetAllNames()
    {
        string sql = $"SELECT name FROM {Table}";
        List<string> LocationsNames = (List<string>)_connection.Query<string>(sql);

        return LocationsNames;
    }

    // public static void Delete(int locId)
    // {
    //     string sql = $"SELECT * FROM Schedule WHERE Location_ID = @Id";
    //     List<ScheduleModel> schedules = (List<ScheduleModel>)_connection.Query<ScheduleModel>(sql, new { Id = locId });

    //     sql = $"SELECT * FROM Orders WHERE Schedule_ID = @Id";
    //     List<OrderModel> orders = [];

    //     foreach (var schedule in schedules)
    //     {
    //         List<OrderModel> temporders = (List<OrderModel>)_connection.Query<OrderModel>(sql, schedule);
    //         foreach (var temporder in temporders)
    //         {
    //             orders.Add(temporder);
    //         }
    //     }

    //     List<AuditoriumModel> auditoriums = [];
    //     foreach (var schedule in schedules)
    //     {
    //         List<AuditoriumModel> tempauditoriums = AuditoriumAcces.GetFromSchedule(schedule);
    //         foreach (var tempauditorium in tempauditoriums)
    //         {
    //             auditoriums.Add(tempauditorium);
    //         }
    //     }

    //     List<SeatModel> seats = [];
    //     foreach (var auditorium in auditoriums)
    //     {
    //         List<SeatModel> tempseats = SeatsAccess.GetFromAuditorium(auditorium);
    //         foreach (var tempseat in tempseats)
    //         {
    //             seats.Add(tempseat);
    //         }
    //     }

    //     sql = $"SELECT * FROM SeatReservations WHERE Order_ID = @Id";
    //     List<ReservationModel> reservations = [];

    //     foreach (var order in orders)
    //     {
    //         List<ReservationModel> tempreservations = (List<ReservationModel>)_connection.Query<ReservationModel>(sql, order);
    //         foreach (var tempreservation in tempreservations)
    //         {
    //             reservations.Add(tempreservation);
    //         }
    //     }

    //     sql = $"SELECT * FROM BoughtSnacks WHERE Reservation_ID = @Id";
    //     List<BoughtSnacksModel> boughtsnacks = [];
    //     foreach (var reservation in reservations)
    //     {
    //         List<BoughtSnacksModel> tempsnacks = (List<BoughtSnacksModel>)_connection.Query<BoughtSnacksModel>(sql, reservation);
    //         foreach (var snacks in tempsnacks)
    //         {
    //             boughtsnacks.Add(snacks);
    //         }
    //     }

    //     List<AssignedRoleModel> assignedroles = AssignedRoleAccess.GetByLocationId(locId);
        

    //     Console.WriteLine("Schedule");
    //     for (int i = 0; i < schedules.Count; i++)
    //     {
    //         Console.WriteLine(schedules[i].Id);
    //     }
    //     Console.WriteLine();

    //     Console.WriteLine("orders");
    //     for (int i = 0; i < orders.Count; i++)
    //     {
    //         Console.WriteLine(orders[i].Id);
    //     }
    //     Console.WriteLine();
    //     Console.WriteLine("reservations");
    //     for (int i = 0; i < reservations.Count; i++)
    //     {
    //         Console.WriteLine(reservations[i].Id);
    //     }
    //     Console.WriteLine();
    //     Console.WriteLine("snacks");
    //     for (int i = 0; i < boughtsnacks.Count; i++)
    //     {
    //         Console.WriteLine(boughtsnacks[i].Id);
    //     }
    //     Console.WriteLine();
    //     Console.WriteLine("assigned roles");
    //     for (int i = 0; i < assignedroles.Count; i++)
    //     {
    //         Console.WriteLine(assignedroles[i].Id);
    //     }
    //     Console.WriteLine();
    //     Console.WriteLine("auditoriums");
    //     for (int i = 0; i < auditoriums.Count; i++)
    //     {
    //         Console.WriteLine(auditoriums[i].Id);
    //     }
    //     Console.WriteLine();
    //     Console.WriteLine("seats");
    //     for (int i = 0; i < seats.Count; i++)
    //     {
    //         Console.WriteLine(seats[i].Id);
    //     }

    //     if (boughtsnacks.Count != 0)
    //     {
    //         foreach (var snack in boughtsnacks)
    //         {
    //             BoughtSnacksAccess.Delete(snack.Id);
    //         }
    //     }

    //     if (reservations.Count != 0)
    //     {
    //         foreach (var reservation in reservations)
    //         {
    //             ReservationAcces.Delete(reservation.Id);
    //         }
    //     }

    //     if (orders.Count != 0)
    //     {
    //         foreach (var order in orders)
    //         {
    //             OrderAccess.Delete(order.Id);
    //         }
    //     }

    //     if (seats.Count != 0)
    //     {
    //         foreach (var seat in seats)
    //         {
    //             SeatsAccess.Delete((int)seat.Id);
    //         }
    //     }

    //     if (auditoriums.Count != 0)
    //     {
    //         foreach (var auditorium in auditoriums)
    //         {
    //             AuditoriumAcces.Delete((int)auditorium.Id);
    //         }
    //     }

    //     if (schedules.Count != 0)
    //     {
    //         foreach (var schedule in schedules)
    //         {
    //             ScheduleAccess.Delete(schedule.Id);
    //         }
    //     }

    //     if (assignedroles.Count != 0)
    //     {
    //         foreach (var assignedrole in assignedroles)
    //         {
    //             AssignedRoleAccess.Delete((int)assignedrole.Id);
    //         }
    //     }

    // }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
        }
}