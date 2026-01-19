export interface TimesheetEntry {
    id: number
    date: string
    hours: number
    description: string
}

export interface WeeklyTimesheet {
    id: number
    weekStartDate: string
    entries: TimesheetEntry[]
}