import sqlite3
import os

# Path to the SQLite database file
db_file = 'project.db'

# Check if the file exists
#if os.path.exists(db_file):
    # Delete the file
    #os.remove(db_file)
    #print(f"{db_file} deleted successfully.")
#else:
    #print(f"{db_file} does not exist.")

# Step 1: Connect to a database (or create it if it doesn't exist)
connection = sqlite3.connect(db_file)

# Step 2: Create a cursor object to execute SQL commands
cursor = connection.cursor()

# Step 3: Create the Account table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Accounts (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT NOT NULL,
    Password TEXT NOT NULL,
    Full_name TEXT NOT NULL,
    Admin BOOLEAN NOT NULL
);
''')

# Step 4: Create the Reservation table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Accounts (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Account_ID INTEGER NOT NULL,
    Schedule_ID INTEGER NOT NULL,
    Seat_Row INTEGER NOT NULL,
    Seat_Collum INTEGER NOT NULL,           
    Status TEXT NOT NULL,
    FOREIGN KEY (Account_ID) REFERENCES Accounts(Account_ID)
    FOREIGN KEY (Schedule_ID) REFERENCES Schedule(Schedule_ID)
);
''')

# Step 5: Create the Auditorium table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Auditorium (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Layout_ID INTEGER NOT NULL,
    Type TEXT,
    Total_seats INTEGER NOT NULL,
    FOREIGN KEY (Layout_ID) REFERENCES Auditorium_layout(Layout_ID)
);
''')

# Step 6: Create the Seats table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Seats (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Auditorium_ID INTEGER NOT NULL,
    Row INTEGER NOT NULL,
    Collum INTEGER NOT NULL,
    Price FLOAT NOT NULL,
    Class INTEGER NOT NULL,
    IsAvailable BOOLEAN DEFAULT 1,
    FOREIGN KEY (Auditorium_ID) REFERENCES Auditorium(Auditorium_ID)
);
''')

# Step 7: Create the Schedule table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Schedule (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Movie_ID INTEGER NOT NULL,
    Auditorium_ID INTEGER NOT NULL,
    FOREIGN KEY (Auditorium_ID) REFERENCES Room(Auditorium_ID)
    FOREIGN KEY (Auditorium_ID) REFERENCES Auditorium(Auditorium_ID)
);
''')

# Step 8: Create the Movie table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Movies (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Author TEXT NOT NULL,
    Description TEXT NOT NULL,
    Length TIME NOT NULL,
    Genre TEXT NOT NULL,
    Movie_ratings TEXT NOT NULL
);
''')

# Step 9: Create the Auditorium_layout table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Auditorium_layout (
    room_id INTEGER,
    row_num INTEGER NOT NULL,
    col_num INTEGER NOT NULL,
    seat_class TINYINT NOT NULL,
    price DECIMAL(4,2) NOT NULL,
    PRIMARY KEY (room_id, row_num, col_num)
);
''')

# Step 10: Commit changes and close the connection
connection.commit()
connection.close()

print("Database and tables created successfully.")