import sqlite3

# Function to insert seat data into the RoomLayout table
def insert_seat(conn, room_id, row_num, col_num1, col_num2, seat_class):
    try:
        if seat_class == 1:
            price = 15.00
        elif seat_class == 2:
            price = 12.50
        elif seat_class == 3:
            price = 10.00
        for col_num in range(col_num1, col_num2+1):
            conn.execute('''
                INSERT INTO Auditorium_layout (room_id, row_num, col_num, seat_class, price)
                VALUES (?, ?, ?, ?, ?)
            ''', (room_id, row_num, col_num, seat_class, price))
            conn.commit()
            print(f"Inserted seat at row {row_num}, column {col_num} into room {room_id}.")
    except sqlite3.IntegrityError as e:
        print(f"Error inserting seat: {e}")

# Function to get user input and insert multiple seats
def input_seats(conn):
    room_id = int(input("Enter room ID: "))
    while True:
        row_num = int(input("Enter seat row number (or -1 to finish): "))
        if row_num == -1:
            break
        col_num1 = int(input("Enter seat column number1: "))
        col_num2 = int(input("Enter seat column number2: "))
        seat_class = int(input("Enter seat class: "))

        insert_seat(conn, room_id, row_num, col_num1, col_num2, seat_class)

# Main function to run the program
def main():
    # Connect to SQLite database (or adjust for your SQL server)
    conn = sqlite3.connect('project.db')  # Change to your preferred database if necessary

    print("Room Layout Entry Tool")
    input_seats(conn)

    # Close the connection when done
    conn.close()
    print("Database connection closed.")

if __name__ == "__main__":
    main()