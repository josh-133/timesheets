export interface TimesheetEntry {
    id: number
    date: string
    startTime: string
    endTime: string
    breakTime: string
    hours: number
    description: string
}

export interface WeeklyTimesheet {
    id: number
    weekStartDate: string
    entries: TimesheetEntry[]
}