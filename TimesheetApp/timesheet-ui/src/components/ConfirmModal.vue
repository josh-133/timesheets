<script setup lang="ts">
    defineProps<{
        show: boolean,
        title: string,
        message: string,
    }>()
    const emit = defineEmits(['confirm', 'cancel'])

</script>

<template>
    <div v-if="show" class="fixed inset-0 bg-gray-100 bg-opacity-50 flex items-center justify-center z-50">

    <!-- Modal box (centered) -->
    <div class="bg-white rounded-lg p-6 text-black">
        <h3>{{ title }}</h3>
        <p>{{ message }}</p>

        <div class="buttons">
            <button class="bg-red-500" @click="$emit('cancel')">Cancel</button>
            <button class="bg-green-500" @click="$emit('confirm')">Confirm</button>
        </div>
    </div>

    </div>
</template>