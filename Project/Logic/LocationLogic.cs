public static class LocationLogic
{
    public static void Add(string name)
    {
        new LocationModel(name);
    }

    public static LocationModel GetById(int id)
    {
        return LocationAccess.GetById(id);
    }

    public static List<LocationModel> GetAll()
    {
        return LocationAccess.GetAll();
    }

    public static List<string> GetAllNames()
    {
        return LocationAccess.GetAllNames();
    }

    public static List<LocationModel> GetAllLocationsWithNoSchedules()
    {
        return LocationAccess.GetAllLocationsWithNoSchedules();
    }

    public static void update(LocationModel location)
    {
        LocationAccess.Update(location);
    }
    public static void Delete(int locationId)
    {
        List<AssignedRoleModel> assignedroles = AssignedRoleAccess.GetAllByLocationId(locationId);
        List<ScheduleModel> schedules = ScheduleAccess.GetByLocationId(locationId);

        List<AuditoriumModel> auditoriums = [];
        foreach (var schedule in schedules)
        {
            List<AuditoriumModel> tempauditoriums = AuditoriumAcces.GetFromSchedule(schedule);
            foreach (var tempauditorium in tempauditoriums)
            {
                auditoriums.Add(tempauditorium);
            }
        }

        List<SeatModel> seats = [];
        foreach (var auditorium in auditoriums)
        {
            List<SeatModel> tempseats = SeatsAccess.GetFromAuditorium(auditorium);
            foreach (var tempseat in tempseats)
            {
                seats.Add(tempseat);
            }
        }

        List<OrderModel> orders = [];
        foreach (var schedule in schedules)
        {
            List<OrderModel> temporders = OrderAccess.GetFromSchedule(schedule);
            foreach (var temporder in temporders)
            {
                orders.Add(temporder);
            }
        }

        List<ReservationModel> reservations = [];
        foreach (var order in orders)
        {
            List<ReservationModel> tempreservations = ReservationAcces.GetFromOrder(order);
            foreach (var tempreservation in tempreservations)
            {
                reservations.Add(tempreservation);
            }
        }

        List<BoughtSnacksModel> boughtsnacks = [];
        foreach (var reservation in reservations)
        {
            List<BoughtSnacksModel> tempsnacks = BoughtSnacksAccess.GetFromReservation(reservation);
            foreach (var snacks in tempsnacks)
            {
                boughtsnacks.Add(snacks);
            }
        }

        if (boughtsnacks.Count != 0)
        {
            foreach (var snack in boughtsnacks)
            {
                BoughtSnacksAccess.Delete(snack.Id);
            }
        }

        if (reservations.Count != 0)
        {
            foreach (var reservation in reservations)
            {
                ReservationAcces.Delete(reservation.Id);
            }
        }

        if (orders.Count != 0)
        {
            foreach (var order in orders)
            {
                OrderAccess.Delete(order.Id);
            }
        }

        if (seats.Count != 0)
        {
            foreach (var seat in seats)
            {
                SeatsAccess.Delete((int)seat.Id);
            }
        }

        if (schedules.Count != 0)
        {
            foreach (var schedule in schedules)
            {
                ScheduleAccess.Delete(schedule.Id);
            }
        }

        if (auditoriums.Count != 0)
        {
            foreach (var auditorium in auditoriums)
            {
                AuditoriumAcces.Delete((int)auditorium.Id);
            }
        }

        if (assignedroles.Count != 0)
        {
            foreach (var assignedrole in assignedroles)
            {
                AssignedRoleAccess.Delete((int)assignedrole.Id);
            }
        }

        LocationAccess.Delete(locationId);
    }

    public static Tuple<string, int> GetLocationInfo()
    {
        List<LocationModel> locations = LocationAccess.GetAll();

        string text = "";

        for (int i = 0; i < locations.Count; i++)
        {
            text += $"[{i + 1}] Name: {locations[i].Name}\n";
        }

        return new(text, locations.Count);
    }

    public static List<LocationModel> GetAllLocations()
    { return LocationAccess.GetAll(); }

}