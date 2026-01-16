<script setup lang="ts">
    import { ref, onMounted } from 'vue';
    import { getTimesheets, deleteTimesheet } from '../services/timesheetApi';
    import type { TimesheetEntry } from '../types/timesheet';
    import { useRouter } from 'vue-router'
    import ConfirmModal from './ConfirmModal.vue'; 
  
    const router = useRouter()
    const loading = ref(true)
    const entries = ref<TimesheetEntry[]>([])
    const error = ref('')
    const showModal = ref(false)
    const entryToDelete = ref<number | null>(null)

    onMounted(async () => {
        try {
            loading.value = true
            entries.value = await getTimesheets()
        } catch (e) {
            error.value = 'Failed to load entries. Please try again.'
        } finally {
            loading.value = false
        }
    })

    function confirmDelete(id: number) {
        entryToDelete.value = id
        showModal.value = true
    }

    async function handleConfirmDelete() {
        if (entryToDelete.value !== null) {
            try {
                await deleteTimesheet(entryToDelete.value)
                entries.value = entries.value.filter(e => e.id !== entryToDelete.value)
            } catch (e) {
                error.value = 'Failed to delete entry. Please try again.'
            }
        }
        showModal.value = false
        entryToDelete.value = null
    }

    function handleCancelDelete() {
        showModal.value = false
        entryToDelete.value = null
    }

    function editEntry(entry: TimesheetEntry) {
        router.push(`/edit/${entry.id}`)
    }
  
  </script>
  
  <template>
    <h2>Timesheet Entries</h2>
    <div v-if="error" class="bg-red-100 text-red-700 p-4 rounded">
        {{ error }}
    </div>
    <div v-if="loading" class="flex items-center justify-center gap-2 py-8">
        <div class="animate-spin h-8 w-8 border-4 border-blue-500 border-t-transparent rounded-full"></div>
        <span>Loading...</span>
    </div>

    <table v-else-if="entries.length > 0">
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
                <button class="bg-red-400 text-white m-1" @click="confirmDelete(entry.id)">Delete</button>
            </td>
        </tr>
    </table>
    <div v-else>
        <p class="text-black">No entries yet</p>
        <router-link class="text-black" to="/create">Create your first entry</router-link>
    </div>

    <ConfirmModal 
        :show="showModal"
        title="Delete Entry"
        message="Are you sure you want to delete this entry?"
        @confirm="handleConfirmDelete"
        @cancel="handleCancelDelete"
    />
  </template>
  
  <style scoped>
  </style>
  