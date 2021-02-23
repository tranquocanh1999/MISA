<template>
  <v-row justify="center">
    <v-btn color="primary" dark @click.stop="dialog = true">
      Open Dialog
    </v-btn>

    <v-dialog v-model="dialog" width="450">
      <v-card>
        <v-card-title class="headline">
          Thông báo
        </v-card-title>

        <v-card-text>
          {{ text }}
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="dialog = false">
            Không
          </v-btn>

          <v-btn color="green darken-1" text @click="dialog = false">
            Đồng ý
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
export default {
  data() {
    return { dialog: false };
  },
  props: {
    text: {
      type: String,
      default: "",
    },
    onCancel: {
      type: Function(),
      default: () => 1,
    },
  },
  onSubmit: {
    type: Function(),
    default: () => 1,
  },
  dialog: {
    type: Boolean,
    default: false,
  },
};
</script>

<style></style>
