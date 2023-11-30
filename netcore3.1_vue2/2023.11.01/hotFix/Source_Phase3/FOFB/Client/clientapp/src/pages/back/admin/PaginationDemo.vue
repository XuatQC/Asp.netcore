<template>
  <v-app>
    <br />
    <h1>Users</h1>
    <br />
    <div style="height: 50px">
      <PaginationComponents
        apiUrl="api/Users/GetDataPagination"
        :pageSize="itemsPerPage"
        @dataWasUpdated="dessertsTable = $event"
      />
    </div>

    <v-data-table
      :headers="headers"
      :items="dessertsTable"
      hide-default-footer
      :items-per-page="itemsPerPage"
    ></v-data-table>
    <br />
  </v-app>
</template>

<script>
import PaginationComponents from "@/components/Pagination.vue";

export default {
  data() {
    return {
      itemsPerPage: 10,
      headers: [
        { text: "UserId", value: "userId" },
        { text: "FullName", value: "fullName" },
        { text: "Password", value: "password" },
        { text: "Phone", value: "phone" },
        { text: "UsUpdateDtTm", value: "usUpdateDtTm" },
      ],
      dessertsTable: [],
    };
  },
  components: {
    PaginationComponents,
  },
  async created() {},
  methods: {},
};
</script>