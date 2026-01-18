const BASE_URL = 'http://localhost:5102/api/timesheetentries'

export async function getTimesheetEntries() {
    const res = await fetch(BASE_URL);
    if (!res.ok) {
        throw new Error('Failed to fetch timesheet entries')
    }
    return res.json();
}

export async function getTimesheetEntry(id: number) {
    const res = await fetch(`${BASE_URL}/${id}`);
    if (!res.ok) {
        throw new Error('Failed to fetch timesheet entry')
    }
    return res.json();
}

export async function createTimesheetEntry(data: any) {
    const res = await fetch(BASE_URL,  {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (!res.ok) {
        throw new Error('Failed to create timesheet entry')
    }
}

export async function updateTimesheetEntry(id: number, data: any) {
    const res = await fetch(`${BASE_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (!res.ok) {
        throw new Error('Failed to update timesheet entry')
    }
}

export async function deleteTimesheetEntry(id: number) {
    const res = await fetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
    });
    if (!res.ok) {
        throw new Error('Failed to delete timesheet entry')
    }
}