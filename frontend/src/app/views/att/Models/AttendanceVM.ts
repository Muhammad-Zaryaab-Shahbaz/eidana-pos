export class AttendanceVM {
    id!: number
    userId?: string
    user?: string
    inTime?: string
    outTime?: string        
    workedHours? :string
    scheduleTime? :string
    date? :  Date = new Date 
    //from? :  Date = new Date 
    //to? :  Date   = new Date
    isActive?: boolean
}
