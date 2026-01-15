<script setup lang="ts">
    import { getTimesheets } from '../services/timesheetApi';
    import type { TimesheetEntry } from '../types/timesheet';
    import { onMounted, ref } from 'vue'
  
    const entries = ref<TimesheetEntry[]>([])

    onMounted(async () => {
        entries.value = await getTimesheets()
    })

  
  </script>
  
  <template>
    <h2>Timesheet Entries</h2>
    <table>
        <tr>
            <th>Date</th>
            <th>Hours</th>
            <th>Description</th>
        </tr>
        <tr v-for="entry in entries" :key="entry.id">
            <td>{{ entry.date }}</td>
            <td>{{ entry.hours }}</td>
            <td>{{ entry.description }}</td>
        </tr>
    </table>
  </template>
  
  <style scoped>
  </style>
  