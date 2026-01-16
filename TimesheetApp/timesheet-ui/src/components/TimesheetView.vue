<script setup lang="ts">
    import { ref, onMounted } from 'vue';
    import { getTimesheets, deleteTimesheet } from '../services/timesheetApi';
    import type { TimesheetEntry } from '../types/timesheet';
    import { useRouter } from 'vue-router'
  
    const router = useRouter();
    const entries = ref<TimesheetEntry[]>([])

    onMounted(async () => {
      entries.value = await getTimesheets()
    })

    async function deleteEntry(id: number) {
        await deleteTimesheet(id);
        entries.value = entries.value.filter(data => data.id != id)
    }

    function editEntry(entry: TimesheetEntry) {
        router.push(`/edit/${entry.id}`)
    }
  
  </script>
  
  <template>
    <h2>Timesheet Entries</h2>
    <table>
        <tr>
            <th>Date</th>
            <th>Hours</th>
            <th>Description</th>
            <th>Options</th>
        </tr>
        <tr v-for="entry in entries" :key="entry.id">
            <td>{{ entry.date }}</td>
            <td>{{ entry.hours }}</td>
            <td>{{ entry.description }}</td>
            <td>
                <button class="bg-blue-400 text-white m-1" @click=editEntry(entry)>Edit</button>
                <button class="bg-red-400 text-white m-1" @click="deleteEntry(entry.id)">Delete</button>
            </td>
        </tr>
    </table>
  </template>
  
  <style scoped>
  </style>
  